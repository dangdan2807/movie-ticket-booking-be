using Microsoft.EntityFrameworkCore;
using MovieTicketBookingBe.src.Models;

namespace MovieTicketBookingBe.src.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext _context;
        private readonly Serilog.ILogger _logger;

        public TokenRepository(AppDbContext context, Serilog.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TokenBlackList> GetTokenByAccessToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return null;
            }
            return await _context.TokenBlackLists.FirstOrDefaultAsync(x => x.AccessToken == accessToken);
        }

        public async Task<TokenBlackList> RevokeToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new Exception("Access token is required");
            }

            var token = await GetTokenByAccessToken(accessToken);
            if (token == null)
            {
                var saveToken = await _context.TokenBlackLists.AddAsync(new TokenBlackList
                {
                    AccessToken = accessToken,
                    CreatedAt = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();
                return saveToken.Entity;
            }
            return token;
        }
    }
}
