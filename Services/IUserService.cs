using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;

namespace MovieTicketBookingBe.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(User user);
        Task<UserDTO> GetUserByPhone(string phone);
    }
}
