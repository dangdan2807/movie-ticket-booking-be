using MovieTicketBookingBe.src.Models;

namespace MovieTicketBookingBe.src.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken(int userId, string token);
        Task<TokenBlackList> RevokeToken(string accessToken);
    }
}
