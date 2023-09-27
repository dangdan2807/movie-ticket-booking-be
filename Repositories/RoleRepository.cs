using Microsoft.EntityFrameworkCore;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;

namespace MovieTicketBookingBe.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role is null");
            }
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
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            return role;
        }

        public async Task<GetRolesDTO> GetRoles(int currentPage = 1, int pageSize = 10, string sort = "ASC")
        {
            var roles = await _context.Roles
                .Where(x => x.IsDeleted == false)
                .OrderBy(b => b.RoleId)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

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
    }
}
