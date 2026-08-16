using TaskTrackerApi.Models;
using TaskTrackerApi.Requests;

namespace TaskTrackerApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);

        Task<User> CreateAsync(CreateUserRequest request);

        Task<bool> UpdateAsync(int id, UpdateUserRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
