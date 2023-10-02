using Microsoft.EntityFrameworkCore;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.Services;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IRoleRepository _roleRepository;
        private readonly Serilog.ILogger _logger;

        public UserRepository(AppDbContext context, IRoleRepository roleRepository, Serilog.ILogger logger)
        {
            _context = context;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<User> CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user info is null");
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
            user.Password = hashedPassword;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            if (id == null)
            {
                throw new Exception("id is null");
            }
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentNullException("phone is null");
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.Phone == phone);
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

            List<User> users = new List<User>();
            if (sort.Equals("DESC"))
            {
                users = await _context.Users
                .OrderByDescending(x => x.CreateAt)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else
            {
                users = await _context.Users
                .OrderBy(x => x.CreateAt)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            var totalUsers = await _context.Users
                .OrderBy(b => b.Id)
                .CountAsync();

            var userList = users.Select(x => new UserDTO
            {
                id = x.Id,
                fullName = x.FullName,
                phone = x.Phone,
                address = x.Address,
                status = x.Status,
                createAt = x.CreateAt,
            }).ToList();

            return new GetUsersDTO
            {
                users = userList,
                pagination = new PaginationDTO
                {
                    currentPage = currentPage,
                    pageSize = pageSize,
                    totalCount = totalUsers,
                }
            };
        }

        public async Task<UserDTO> UpdateUserById(int userId, UpdateUserVM updateUserVM)
        {
            // Check for valid userId
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid userId");
            }

            // Check if the user exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            // Check if the phone is already in use
            if (!string.IsNullOrEmpty(updateUserVM.phone))
            {
                var existingPhoneUser = await _context.Users.FirstOrDefaultAsync(u => u.Phone == updateUserVM.phone);
                if (existingPhoneUser != null && existingPhoneUser.Id != userId)
                {
                    throw new Exception("Phone is already in use");
                }
            }

            // Update user properties if provided
            if (!string.IsNullOrEmpty(updateUserVM.fullName))
            {
                existingUser.FullName = updateUserVM.fullName;
            }
            if (!string.IsNullOrEmpty(updateUserVM.address))
            {
                existingUser.Address = updateUserVM.address;
            }
            if (!string.IsNullOrEmpty(updateUserVM.phone))
            {
                existingUser.Phone = updateUserVM.phone;
            }
            if (updateUserVM.status != null)
            {
                existingUser.Status = updateUserVM.status;
            }

            // Delete old roles
            var deleteIds = await _context.UserRoles
                .Where(x => x.UserId == userId && !updateUserVM.roleIds.Contains(x.RoleId))
                .ToListAsync();
            if (deleteIds.Any())
            {
                _context.UserRoles.RemoveRange(deleteIds);
                await _context.SaveChangesAsync();
            }

            // Get current roles
            var roles = await _roleRepository.GetRolesByUserId(userId);

            // Add new roles
            var addIds = updateUserVM.roleIds.Except(roles.Select(r => r.RoleId)).ToList();
            if (addIds.Any())
            {
                _context.UserRoles.AddRange(addIds.Select(id => new UserRole { UserId = userId, RoleId = id }));
            }

            // Save changes
            await _context.SaveChangesAsync();

            // Retrieve updated roles
            roles = await _roleRepository.GetRolesByUserId(userId);

            return new UserDTO
            {
                id = existingUser.Id,
                fullName = existingUser.FullName,
                address = existingUser.Address,
                phone = existingUser.Phone,
                status = existingUser.Status,
                createAt = existingUser.CreateAt,
                roles = roles.Select(r => new RoleDTO
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

        public async Task<User?> UpdateProfile(int userId, UpdateProfileVM updateProfileVM)
        {
            if (userId <= 0)
            {
                throw new Exception("Id is invalid");
            }

            var user = await GetUserById(userId);
            if (user == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(updateProfileVM.fullName))
            {
                user.FullName = updateProfileVM.fullName;
            }
            if (!string.IsNullOrEmpty(updateProfileVM.address))
            {
                user.Address = updateProfileVM.address;
            }
            if (!string.IsNullOrEmpty(updateProfileVM.phone))
            {
                user.Phone = updateProfileVM.phone;
            }

            var existingPhoneUser = await GetUserByPhone(updateProfileVM.phone);
            if (existingPhoneUser != null && existingPhoneUser.Id != userId)
            {
                throw new Exception("Phone is already in use");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(updateProfileVM.confirmPassword, user.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Wrong password");
            }

            user.UpdateAt = DateTime.Now;
            user.UpdateBy = userId;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> UpdatePassword(int userId, UpdatePasswordVM updatePasswordVM)
        {
            if (userId <= 0)
            {
                throw new Exception("Id is invalid");
            }

            var user = await GetUserById(userId);
            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(updatePasswordVM.oldPassword, user.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Wrong password");
            }

            if (string.IsNullOrEmpty(updatePasswordVM.newPassword))
            {
                throw new Exception("New password is required");
            }

            if (string.IsNullOrEmpty(updatePasswordVM.confirmNewPassword))
            {
                throw new Exception("Confirm password is required");
            }

            if (updatePasswordVM.newPassword.Equals(updatePasswordVM.confirmNewPassword))
            {
                throw new Exception("Confirm password is not match");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(updatePasswordVM.newPassword);
            user.UpdateAt = DateTime.Now;
            user.UpdateBy = userId;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
