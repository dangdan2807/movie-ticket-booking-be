using Microsoft.EntityFrameworkCore;
using MovieTicketBookingBe.Models;

namespace MovieTicketBookingBe.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user is null");
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
            user.Password = hashedPassword;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id is null");
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentNullException("phone is null");
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.Phone == phone);
        }

        public Task<User> UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
