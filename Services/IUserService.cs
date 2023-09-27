using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(User user);
        Task<UserDTO> GetUserByPhone(string phone);
        Task<LoginDTO> Login(LoginViewModel loginViewModel);
    }
}
