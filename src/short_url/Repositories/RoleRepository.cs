using Microsoft.EntityFrameworkCore;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;
using MySqlConnector;

namespace MovieTicketBookingBe.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly Serilog.ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public RoleRepository(AppDbContext context, Serilog.ILogger logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Role> CreateRole(int userId, Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role is null");
            }
            role.CreateAt = DateTime.Now;
            role.CreateBy = userId;

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> GetRoleById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }

            var role = await _context.Roles
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.RoleId == id);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            return role;
        }

        public async Task<Role> GetRoleByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new Exception("code is empty or null");
            }

            var role = await _context.Roles
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.RoleCode == code);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            return role;
        }

        public async Task<GetRolesDTO> GetRoles(PaginationVM paginationVM, string? keyword, bool? status = true)
        {
            List<Role> roles = new List<Role>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                int skip = (paginationVM.currentPage - 1) * paginationVM.pageSize;
                int limit = paginationVM.pageSize;
                string queryString = @"SELECT * FROM roles WHERE (role_code LIKE @keyword OR role_name LIKE @keyword) and is_deleted = 0 and status = @status ORDER BY Create_At " +
                    paginationVM.sort + " LIMIT @skip, @limit";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
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
                        Role role = new Role
                        {
                            RoleId = reader.GetInt32("role_id"),
                            RoleName = reader.GetString("role_name"),
                            RoleCode = reader.GetString("role_code"),
                            Description = string.IsNullOrEmpty(reader.GetString("description")) ? "" : reader.GetString("description"),
                            Priority = reader.GetInt32("priority"),
                            Status = reader.GetBoolean("status"),
                            CreateAt = reader.GetDateTime("create_at"),
                            CreateBy = reader.GetInt32("create_by"),
                        };

                        roles.Add(role);
                    }
                }
                connection.Close();
            }

            var totalCount = await _context.Roles
                .Where(x => x.IsDeleted == false)
                .OrderBy(b => b.RoleId)
                .CountAsync();

            var rolesList = roles.Select(x => new RoleDTO
            {
                roleId = x.RoleId,
                roleName = x.RoleName,
                roleCode = x.RoleCode,
                description = x.Description,
                priority = x.Priority,
                status = x.Status,
                createAt = x.CreateAt,
            }).ToList();

            return new GetRolesDTO
            {
                roles = rolesList,
                pagination = new PaginationDTO
                {
                    currentPage = paginationVM.currentPage,
                    pageSize = paginationVM.pageSize,
                    totalCount = totalCount,
                }
            };
        }

        public async Task<Role> UpdateRoleById(int id, int userId, Role role)
        {
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }
            if (role == null)
            {
                throw new Exception("Role is null");
            }

            var existingRole = await _context.Roles
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.RoleId == id);
            if (existingRole == null)
            {
                throw new Exception("Role not found");
            }

            existingRole.RoleId = existingRole.RoleId;
            existingRole.RoleName = role.RoleName;
            existingRole.RoleCode = role.RoleCode;
            existingRole.Description = role.Description;
            existingRole.Status = role.Status;
            existingRole.UpdateBy = userId;
            existingRole.UpdateAt = DateTime.Now;

            _context.Entry(existingRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return existingRole;
        }

        public async Task<Role> DeleteRoleById(int id, int userId)
        {
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }

            var existingRole = await _context.Roles.Where(b => b.IsDeleted == false).FirstOrDefaultAsync(x => x.RoleId == id);
            if (existingRole == null)
            {
                throw new Exception("Role not found");
            }

            existingRole.IsDeleted = true;
            existingRole.DeleteAt = DateTime.Now;
            existingRole.UpdateBy = userId;
            existingRole.UpdateAt = DateTime.Now;

            _context.Entry(existingRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return existingRole;
        }

        // =================================== USER ROLE ===================================
        public async Task<UserRole> CreateUserRole(int userId, string roleCode)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode);
            if (role == null)
            {
                throw new Exception("Role not found");
            }

            var userRole = await _context.UserRoles.FirstOrDefaultAsync(u =>
                u.UserId == userId &&
                u.RoleId == role.RoleId
            );
            if (userRole != null)
            {
                throw new Exception("User role already exists");
            }

            userRole = new UserRole
            {
                UserId = userId,
                RoleId = role.RoleId
            };
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
            return userRole;
        }

        public async Task<List<Role>> GetRolesByUserId(int userId)
        {
            var userRoles = await _context.UserRoles
                .Where(u => u.UserId == userId)
                .ToListAsync();
            if (userRoles.Count == 0)
            {
                throw new Exception("User role not found");
            }
            foreach (var item in userRoles)
            {
                item.Role = await GetRoleById(item.RoleId);
            }
            return userRoles.Select(u => u.Role).ToList();
        }
    }
}
