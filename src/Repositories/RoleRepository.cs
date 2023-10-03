using Microsoft.EntityFrameworkCore;
using MovieTicketBookingBe.src.Models.DTO;
using MovieTicketBookingBe.src.Models;
using MovieTicketBookingBe.src.Models.DTO;

namespace MovieTicketBookingBe.src.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly Serilog.ILogger _logger;

        public RoleRepository(AppDbContext context, Serilog.ILogger logger)
        {
            _context = context;
            _logger = logger;
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

        public async Task<GetRolesDTO> GetRoles(int currentPage = 1, int pageSize = 10, string sort = "ASC")
        {
            List<Role> roles = new List<Role>();
            if (sort.Equals("DESC"))
            {
                roles = await _context.Roles
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(b => b.RoleId)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else
            {
                roles = await _context.Roles
                .Where(x => x.IsDeleted == false)
                .OrderBy(b => b.RoleId)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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
                status = x.Status,
                createAt = x.CreateAt,
            }).ToList();

            return new GetRolesDTO
            {
                roles = rolesList,
                pagination = new PaginationDTO
                {
                    currentPage = currentPage,
                    pageSize = pageSize,
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
