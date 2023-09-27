using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.Repositories;

namespace MovieTicketBookingBe.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleDTO> CreateRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role is null");
            }
            var newRole = await _roleRepository.CreateRole(role);
            return new RoleDTO
            {
                roleId = newRole.RoleId,
                roleName = newRole.RoleName,
                roleCode = newRole.RoleCode,
                status = newRole.Status,
                createAt = newRole.CreateAt,
            };
        }

        public async Task<RoleDTO> GetRoleById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }
            var role = await _roleRepository.GetRoleById(id);
            return new RoleDTO
            {
                roleId = role.RoleId,
                roleName = role.RoleName,
                roleCode = role.RoleCode,
                status = role.Status,
                createAt = role.CreateAt,
            };
        }

        public async Task<GetRolesDTO> GetRoles(int currentPage = 1, int pageSize = 10, string sort = "ASC")
        {
            if (currentPage <= 0 || pageSize <= 0)
            {
                throw new Exception("Page is invalid");
            }
            if (sort != "ASC" && sort != "DESC")
            {
                throw new Exception("Sort is invalid");
            }
            return await _roleRepository.GetRoles(currentPage, pageSize, sort);
        }
    }
}
