using MovieTicketBookingBe.src.Models;
using MovieTicketBookingBe.src.Models.DTO;

namespace MovieTicketBookingBe.src.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> CreateRole(int userId, Role role);
        Task<Role> GetRoleById(int id);
        Task<Role> GetRoleByCode(string code);
        Task<GetRolesDTO> GetRoles(int currentPage = 1, int pageSize = 10, string sort = "ASC");
        Task<Role> UpdateRoleById(int id, int userId, Role role);
        Task<Role> DeleteRoleById(int id, int userId);

        Task<UserRole> CreateUserRole(int userId, string roleCode);
        Task<List<Role>> GetRolesByUserId(int userId);
    }
}
