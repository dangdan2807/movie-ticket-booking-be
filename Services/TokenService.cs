using Microsoft.IdentityModel.Tokens;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieTicketBookingBe.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config, IUserRepository userRepository) {
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<string> GenerateAccessToken(User user)
        {
            // Nếu xác thực thành công thì trả về chuỗi JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var sercetKey = Encoding.ASCII.GetBytes(_config["Jwt:SercetKey"] ?? "");
            //var role = user.isAdmin ? "Admin" : "User";
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    //new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(sercetKey),
                               SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }

        public async Task<string> GenerateRefreshToken(int userId, string token)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                var refreshToken = Convert.ToBase64String(randomNumber);
                var now = DateTime.UtcNow;
                //var tokenResponse = new TokenResponse
                //{
                //    userId = userId,
                //    refreshToken = refreshToken,
                //    accessToken = token,
                //    expRefreshToken = DateTime.UtcNow.AddDays(7),
                //};
                //await _tokenRepository.AddToken(tokenResponse);
                return refreshToken;
            }
        }
    }
}
