using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Services
{
    public interface IRoleService
    {
        Task<RoleDTO> CreateRole(int userId, Role role);
        Task<RoleDTO> GetRoleById(int id);
        Task<RoleDTO> GetRoleByCode(string code);
        Task<GetRolesDTO> GetRoles(PaginationVM paginationVM, string? keyword, bool? status = true);
        Task<RoleDTO> UpdateRoleById(int id, int userId, Role role);
        Task<RoleDTO> DeleteRoleById(int id, int userId);
    }
}
