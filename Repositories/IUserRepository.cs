using MovieTicketBookingBe.Models;

namespace MovieTicketBookingBe.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> UpdateUser();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByPhone(string phone);
    }
}
