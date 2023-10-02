using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.Services;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserById(int id);
        Task<User> GetUserByPhone(string phone);
        Task<GetUsersDTO> GetUsers(int currentPage = 1, int pageSize = 10, string sort = "ASC");
        Task<UserDTO> UpdateUserById(int id, UpdateUserVM updateUserVM);
        Task<User?> UpdateProfile(int userId, UpdateProfileVM updateProfileVM);
        Task<User?> UpdatePassword(int userId, UpdatePasswordVM updatePasswordVM);
    }
}
