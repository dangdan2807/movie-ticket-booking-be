using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(User user);
        Task<UserDTO?> GetUserById(int id);
        Task<UserDTO?> GetUserByEmail(string email);
        Task<GetUsersDTO> GetUsers(PaginationVM paginationVM, string? keyword = "", bool? status = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<LoginDTO> Login(LoginViewModel loginViewModel);
        Task<UserDTO> UpdateUserById(int id, int userIdUpdate, UpdateUserVM updateUserVM);
        Task<UserDTO> UpdateProfile(int userId, UpdateProfileVM updateProfileVM);
        Task<UserDTO> UpdatePassword(int userId, UpdatePasswordVM updatePasswordVM);
        Task<int> TotalUsers();
    }
}
