using Microsoft.EntityFrameworkCore;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;
using MySqlConnector;

namespace MovieTicketBookingBe.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IRoleRepository _roleRepository;
        private readonly Serilog.ILogger _logger;
        private readonly IConfiguration _configuration;

        private string _connectionString;

        public UserRepository(AppDbContext context, IRoleRepository roleRepository, Serilog.ILogger logger, IConfiguration configuration)
        {
            _context = context;
            _roleRepository = roleRepository;
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
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

        public async Task<User> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("email is null");
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<GetUsersDTO> GetUsers(PaginationVM paginationVM, string? keyword = "", bool? status = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            DateTime currentDate = DateTime.Now;
            DateTime sDate = currentDate.Date;
            DateTime eDate = currentDate.Date.AddDays(1);
            if (startDate.HasValue)
            {
                sDate = startDate.Value.Date;
            }
            if (endDate.HasValue)
            {
                eDate = endDate.Value.Date;
            }
            _logger.Information(sDate + " " + eDate + "");
            _logger.Information(startDate.Value.Date + " " + endDate.Value.Date + "");

            if (paginationVM.currentPage <= 0)
            {
                throw new Exception("Current page is invalid");
            }

            if (paginationVM.pageSize <= 0)
            {
                throw new Exception("Page size is invalid");
            }

            if (string.IsNullOrEmpty(paginationVM.sort.ToString()))
            {
                throw new Exception("Sort is invalid");
            }
            else if (paginationVM.sort.Equals("ASC") && paginationVM.sort.Equals("DESC"))
            {
                throw new Exception("Sort is invalid");
            }

            List<User> users = new List<User>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                int skip = (paginationVM.currentPage - 1) * paginationVM.pageSize;
                int limit = paginationVM.pageSize;
                string queryString = @"SELECT * FROM Users WHERE (full_name LIKE @keyword OR Email LIKE @keyword) " +
                    "and Create_At between @startDate and @endDate and status = @status ORDER BY Create_At " +
                    paginationVM.sort + " LIMIT @skip, @limit";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                command.Parameters.AddWithValue("@startDate", sDate);
                command.Parameters.AddWithValue("@endDate", eDate);
                if (status != null)
                {
                    command.Parameters.AddWithValue("@status", status);
                }
                command.Parameters.AddWithValue("@skip", skip);
                command.Parameters.AddWithValue("@limit", limit);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("user_id")),
                            FullName = reader.GetString(reader.GetOrdinal("full_name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            Status = reader.GetBoolean(reader.GetOrdinal("Status")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            CreateAt = reader.GetDateTime(reader.GetOrdinal("Create_At")),
                        };

                        users.Add(user);
                    }
                }
                connection.Close();
            }

            var totalUsers = await _context.Users
                .Where(x => x.FullName.Contains(keyword) || x.Email.Contains(keyword))
                .OrderBy(b => b.Id)
                .CountAsync();

            var userList = users.Select(x => new UserDTO
            {
                id = x.Id,
                fullName = x.FullName,
                email = x.Email,
                address = x.Address,
                status = x.Status,
                createAt = x.CreateAt,
            }).ToList();

            return new GetUsersDTO
            {
                users = userList,
                pagination = new PaginationDTO
                {
                    currentPage = paginationVM.currentPage,
                    pageSize = paginationVM.pageSize,
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

            // Check if the email is already in use
            if (!string.IsNullOrEmpty(updateUserVM.email))
            {
                var existingEmailUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == updateUserVM.email);
                if (existingEmailUser != null && existingEmailUser.Id != userId)
                {
                    throw new Exception("Email is already in use");
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
            if (!string.IsNullOrEmpty(updateUserVM.email))
            {
                existingUser.Email = updateUserVM.email;
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
                email = existingUser.Email,
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
            if (!string.IsNullOrEmpty(updateProfileVM.email))
            {
                user.Email = updateProfileVM.email;
            }

            var existingEmailUser = await GetUserByEmail(updateProfileVM.email);
            if (existingEmailUser != null && existingEmailUser.Id != userId)
            {
                throw new Exception("Email is already in use");
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

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(updatePasswordVM.currentPassword, user.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Wrong password");
            }

            if (string.IsNullOrEmpty(updatePasswordVM.newPassword))
            {
                throw new Exception("New password is required");
            }

            if (string.IsNullOrEmpty(updatePasswordVM.confirmPassword))
            {
                throw new Exception("Confirm password is required");
            }

            if (!updatePasswordVM.newPassword.Equals(updatePasswordVM.confirmPassword))
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
