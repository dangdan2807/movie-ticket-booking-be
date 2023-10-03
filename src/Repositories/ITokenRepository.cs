using MovieTicketBookingBe.src.Models;

namespace MovieTicketBookingBe.src.Repositories
{
    public interface ITokenRepository
    {
        Task<TokenBlackList> RevokeToken(string accessToken);
        Task<TokenBlackList> GetTokenByAccessToken(string accessToken);
    }
}
