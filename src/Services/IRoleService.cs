using MovieTicketBookingBe.src.Models;
using MovieTicketBookingBe.src.Models.DTO;

namespace MovieTicketBookingBe.src.Services
{
    public interface IRoleService
    {
        Task<RoleDTO> CreateRole(int userId, Role role);
        Task<RoleDTO> GetRoleById(int id);
        Task<RoleDTO> GetRoleByCode(string code);
        Task<GetRolesDTO> GetRoles(int currentPage = 1, int pageSize = 10, string sort = "ASC");
        Task<RoleDTO> UpdateRoleById(int id, int userId, Role role);
        Task<RoleDTO> DeleteRoleById(int id, int userId);
    }
}
