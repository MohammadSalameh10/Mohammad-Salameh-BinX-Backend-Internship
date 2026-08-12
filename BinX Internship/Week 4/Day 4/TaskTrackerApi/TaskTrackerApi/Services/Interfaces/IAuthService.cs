using Microsoft.AspNetCore.Identity;
using TaskTrackerApi.Requests;

namespace TaskTrackerApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequest request);

        Task<string?> LoginAsync(LoginRequest request);
        Task<IdentityResult> AddUserToRoleAsync(string email, string role);
    }
}