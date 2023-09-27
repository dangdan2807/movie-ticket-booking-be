using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.Repositories;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserDTO> CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user is null");
            }
            var existingUser = await _userRepository.GetUserByPhone(user.Phone);
            if (existingUser != null)
            {
                throw new Exception("Phone number already exists");
            }
            var newUser = await _userRepository.CreateUser(user);

            return new UserDTO
            {
                id = newUser.Id,
                fullName = newUser.FullName,
                phone = newUser.Phone,
                address = newUser.Address,
                status = newUser.Status,
            };
        }

        public async Task<UserDTO> GetUserByPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentNullException("phone is null");
            }
            var user = await _userRepository.GetUserByPhone(phone);
            if (user == null)
            {
                return null;
            }
            return new UserDTO
            {
                id = user.Id,
                fullName = user.FullName,
                address = user.Address,
                phone = user.Phone,
                status = user.Status,
                CreateAt = user.CreateAt,
            };
        }

        public async Task<LoginDTO> Login(LoginViewModel loginViewModel)
        {
            User? user = await _userRepository.GetUserByPhone(loginViewModel.phone);
            if (user == null)
            {
                throw new ArgumentException("Wrong phone or password");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginViewModel.password, user.Password);
            if (!isPasswordValid)
            {
                throw new ArgumentException("Wrong phone or password");
            }

            var accessToken = await _tokenService.GenerateAccessToken(user);
            var refreshToken = await _tokenService.GenerateRefreshToken(user.Id, accessToken);

            return new LoginDTO
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            };
        }
    }
}
