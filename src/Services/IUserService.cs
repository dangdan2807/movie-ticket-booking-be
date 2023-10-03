using MovieTicketBookingBe.src.Models;
using MovieTicketBookingBe.src.Models.DTO;
using MovieTicketBookingBe.src.ViewModels;

namespace MovieTicketBookingBe.src.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(User user);
        Task<UserDTO?> GetUserById(int id);
        Task<UserDTO?> GetUserByPhone(string phone);
        Task<GetUsersDTO> GetUsers(int currentPage = 1, int pageSize = 10, string sort = "ASC");
        Task<LoginDTO> Login(LoginViewModel loginViewModel);
        Task<UserDTO> UpdateUserById(int id, int userIdUpdate, UpdateUserVM updateUserVM);
        Task<UserDTO> UpdateProfile(int userId, UpdateProfileVM updateProfileVM);
        Task<UserDTO> UpdatePassword(int userId, UpdatePasswordVM updatePasswordVM);
    }
}
