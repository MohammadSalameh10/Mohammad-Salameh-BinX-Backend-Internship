using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.Models;
using TaskTrackerApi.Requests;
using TaskTrackerApi.Services.Interfaces;

namespace TaskTrackerApi.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> CreateAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateUserRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);

            if (user is null)
            {
                return false;
            }

            user.Name = request.Name;
            user.Email = request.Email;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);

            if (user is null)
            {
                return false;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
