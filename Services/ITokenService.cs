using MovieTicketBookingBe.Models;

namespace MovieTicketBookingBe.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken(int userId, string token);
    }
}
