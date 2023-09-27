using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;

namespace MovieTicketBookingBe.Services
{
    public interface IRoleService
    {
        Task<RoleDTO> CreateRole(Role role);
        Task<RoleDTO> GetRoleById(int id);
        Task<GetRolesDTO> GetRoles(int currentPage = 1, int pageSize = 10, string sort = "ASC");
    }
}
