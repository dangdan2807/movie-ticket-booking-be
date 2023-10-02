using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.Repositories;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRoleRepository _roleRepository;
        private readonly Serilog.ILogger _logger;

        public UserService(IUserRepository userRepository, ITokenService tokenService, IRoleRepository roleRepository, Serilog.ILogger logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<UserDTO> CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user is null");
            }

            var existingUser = await _userRepository.GetUserByPhone(user.Phone);
            if (existingUser != null)
            {
                throw new Exception("Phone number already exists");
            }

            User newUser = await _userRepository.CreateUser(user);
            if (newUser != null)
            {
                var role = await _roleRepository.GetRoleByCode("MEMBER");
                await _roleRepository.CreateUserRole(newUser.Id, role.RoleCode);
            }

            return new UserDTO
            {
                id = newUser.Id,
                fullName = newUser.FullName,
                phone = newUser.Phone,
                address = newUser.Address,
                status = newUser.Status,
            };
        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }

            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return null;
            }

            var roles = await _roleRepository.GetRolesByUserId(user.Id);
            var rolesDTO = roles.Select(r => new RoleDTO
            {
                roleId = r.RoleId,
                roleCode = r.RoleCode,
                roleName = r.RoleName,
                description = r.Description,
                status = r.Status,
                createAt = r.CreateAt,
            }).ToList();


            return new UserDTO
            {
                id = user.Id,
                fullName = user.FullName,
                address = user.Address,
                phone = user.Phone,
                status = user.Status,
                createAt = user.CreateAt,
                roles = rolesDTO
            };
        }

        public async Task<UserDTO?> GetUserByPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentNullException("phone is null");
            }
            var user = await _userRepository.GetUserByPhone(phone);
            if (user == null)
            {
                return null;
            }
            return new UserDTO
            {
                id = user.Id,
                fullName = user.FullName,
                address = user.Address,
                phone = user.Phone,
                status = user.Status,
                createAt = user.CreateAt,
            };
        }

        public async Task<GetUsersDTO> GetUsers(int currentPage = 1, int pageSize = 10, string sort = "ASC")
        {
            if (currentPage <= 0)
            {
                throw new Exception("Current page is invalid");
            }

            if (pageSize <= 0)
            {
                throw new Exception("Page size is invalid");
            }

            if (string.IsNullOrEmpty(sort))
            {
                throw new Exception("Sort is invalid");
            }
            else if (sort != "ASC" && sort != "DESC")
            {
                throw new Exception("Sort is invalid");
            }
            return await _userRepository.GetUsers(currentPage, pageSize, sort);
        }

        public async Task<LoginDTO> Login(LoginViewModel loginViewModel)
        {
            User? user = await _userRepository.GetUserByPhone(loginViewModel.phone);
            if (user == null)
            {
                throw new ArgumentException("Wrong phone or password");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginViewModel.password, user.Password);
            if (!isPasswordValid)
            {
                throw new ArgumentException("Wrong phone or password");
            }

            var accessToken = await _tokenService.GenerateAccessToken(user);
            var refreshToken = await _tokenService.GenerateRefreshToken(user.Id, accessToken);

            return new LoginDTO
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            };
        }

        public async Task<UserDTO> UpdateUserById(int id, int userIdUpdate, UpdateUserVM updateUserVM)
        {
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }
            if (updateUserVM == null)
            {
                throw new Exception("UpdateUserVM is null");
            }
            if (updateUserVM.roleIds == null)
            {
                throw new Exception("RoleIds is null");
            }
            if (updateUserVM.roleIds.Count == 0)
            {
                throw new Exception("RoleIds is empty");
            }
            if (updateUserVM.roleIds.Any(r => r <= 0))
            {
                throw new Exception("RoleIds is invalid");
            }
            var userUpdate = await _userRepository.GetUserById(userIdUpdate);
            if (userUpdate == null)
            {
                throw new Exception("User update not found");
            }
            var roleUserUpdate = await _roleRepository.GetRolesByUserId(userIdUpdate);
            if (roleUserUpdate.Count <= 0)
            {
                throw new Exception("Role not found");
            }

            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var roleUser = await _roleRepository.GetRolesByUserId(id);
            if (roleUser.Count <= 0)
            {
                throw new Exception("Role not found");
            }

            if (roleUser.Any(r => r.RoleCode == "ADMIN"))
            {
                if (!roleUserUpdate.Any(r => r.RoleCode == "ADMIN"))
                {
                    throw new Exception("You can't update admin");
                }
            }

            return await _userRepository.UpdateUserById(id, updateUserVM);
        }

        public async Task<UserDTO> UpdateProfile(int userId, UpdateProfileVM updateProfileVM)
        {
            if (userId <= 0)
            {
                throw new Exception("Id is invalid");
            }
            if (updateProfileVM == null)
            {
                throw new Exception("UpdateProfileVM is null");
            }

            var user = await _userRepository.UpdateProfile(userId, updateProfileVM);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var role = await _roleRepository.GetRolesByUserId(userId);
            return new UserDTO
            {
                id = user.Id,
                fullName = user.FullName,
                address = user.Address,
                phone = user.Phone,
                status = user.Status,
                createAt = user.CreateAt,
                roles = role.Select(r => new RoleDTO
                {
                    roleId = r.RoleId,
                    roleCode = r.RoleCode,
                    roleName = r.RoleName,
                    description = r.Description,
                    status = r.Status,
                    createAt = r.CreateAt,
                }).ToList()
            };
        }

        public async Task<UserDTO> UpdatePassword(int userId, UpdatePasswordVM updatePasswordVM)
        {
            if (userId <= 0)
            {
                throw new Exception("Id is invalid");
            }
            if (updatePasswordVM == null)
            {
                throw new Exception("UpdatePasswordVM is null");
            }

            var user = await _userRepository.UpdatePassword(userId, updatePasswordVM);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return new UserDTO
            {
                id = user.Id,
                fullName = user.FullName,
                address = user.Address,
                phone = user.Phone,
                status = user.Status,
                createAt = user.CreateAt,
            };
        }
    }
}
