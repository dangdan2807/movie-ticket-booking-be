using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(User user);
        Task<UserDTO?> GetUserById(int id);
        Task<UserDTO?> GetUserByPhone(string phone);
        Task<GetUsersDTO> GetUsers(PaginationVM paginationVM, string? keyword = "", bool? status = null);
        Task<LoginDTO> Login(LoginViewModel loginViewModel);
        Task<UserDTO> UpdateUserById(int id, int userIdUpdate, UpdateUserVM updateUserVM);
        Task<UserDTO> UpdateProfile(int userId, UpdateProfileVM updateProfileVM);
        Task<UserDTO> UpdatePassword(int userId, UpdatePasswordVM updatePasswordVM);
    }
}
