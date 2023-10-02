using Microsoft.IdentityModel.Tokens;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MovieTicketBookingBe.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _config;
        private readonly Serilog.ILogger _logger;

        public TokenService(
            IConfiguration config, 
            IUserRepository userRepository, 
            ITokenRepository tokenRepository,
            IRoleRepository roleRepository,
            Serilog.ILogger logger
        )
        {
            _config = config;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<string> GenerateAccessToken(User user)
        {
            var roles = await _roleRepository.GetRolesByUserId(user.Id);
            if (roles.Count == 0)
            {
                throw new Exception("User not found");
            }

            // Nếu xác thực thành công thì trả về chuỗi JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var sercetKey = Encoding.ASCII.GetBytes(_config["Jwt:SercetKey"] ?? "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(sercetKey),
                               SecurityAlgorithms.HmacSha256Signature)
            };
            foreach (var role in roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.RoleCode));
            }
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
                return refreshToken;
            }
        }

        public async Task<TokenBlackList> RevokeToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new Exception("Access token is required");
            }

            var token = await _tokenRepository.RevokeToken(accessToken);
            if (token == null)
            {
                throw new Exception("Access token is invalid");
            }
            return token;
        }
    }
}
