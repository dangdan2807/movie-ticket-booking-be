﻿using Microsoft.EntityFrameworkCore;
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
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
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