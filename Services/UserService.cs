using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.Repositories;

namespace MovieTicketBookingBe.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                Id = newUser.Id,
                FullName = newUser.FullName,
                Phone = newUser.Phone,
                Address = newUser.Address,
                Status = newUser.Status,
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
                Id = user.Id,
                FullName = user.FullName,
                Address = user.Address,
                Phone = user.Phone,
                Status = user.Status,
                CreateAt = user.CreateAt,
                UpdateAt = user.UpdateAt,
                UpdateBy = user.UpdateBy,
            };
        }
    }
}
