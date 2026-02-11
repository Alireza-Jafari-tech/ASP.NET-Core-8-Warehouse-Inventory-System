using Application.DTOs;

namespace Infrastructure.Identity
{
    public interface IIdentityService
    {
        Task<(string? userId, string? createError)> CreateUserAsync(CreateUserRequest userRequest);
        // Task<ApplicationUser?> GetUserByIdAsync(string userId);
        Task<bool> SignInAsync(string email, string password, bool rememberMe);
        Task SignOutAsync();
        Task<bool> AddToRoleAsync(string userId, string role);
        Task<string?> GetUserRoleAsync(string userId);
        Task<bool> RoleExistsAsync(string role);
    }
}