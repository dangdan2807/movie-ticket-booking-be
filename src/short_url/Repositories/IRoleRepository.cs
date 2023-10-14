using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> CreateRole(int userId, Role role);
        Task<Role> GetRoleById(int id);
        Task<Role> GetRoleByCode(string code);
        Task<GetRolesDTO> GetRoles(PaginationVM paginationVM, string? keyword, bool? status = true);
        Task<Role> UpdateRoleById(int id, int userId, Role role);
        Task<Role> DeleteRoleById(int id, int userId);

        Task<UserRole> CreateUserRole(int userId, string roleCode);
        Task<List<Role>> GetRolesByUserId(int userId);
    }
}
