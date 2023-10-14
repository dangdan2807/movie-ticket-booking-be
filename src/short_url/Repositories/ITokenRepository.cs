using MovieTicketBookingBe.Models;

namespace MovieTicketBookingBe.Repositories
{
    public interface ITokenRepository
    {
        Task<TokenBlackList> RevokeToken(string accessToken);
        Task<TokenBlackList> GetTokenByAccessToken(string accessToken);
    }
}
