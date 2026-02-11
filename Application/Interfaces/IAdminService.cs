using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<bool> CreateAdminAsync(CreateUserRequest userRequest);
        Task<bool> SignInAdminAsync(string email, string password, bool rememberMe = false);
        Task SignOutAdminAsync();
        Task<bool> AddToRoleAsync(string userId, string roleName);
        Task<string?> GetAdminRoleAsync(string userId);
    }
}