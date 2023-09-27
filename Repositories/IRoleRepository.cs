using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;

namespace MovieTicketBookingBe.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> CreateRole(Role role);
        Task<Role> GetRoleById(int id);
        Task<GetRolesDTO> GetRoles(int currentPage = 1, int pageSize = 10, string sort = "ASC");
    }
}
