using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CreateUserRequest userRequest);
        Task<bool> SignInCustomerAsync(string email, string password, bool rememberMe = false);
        Task SignOutCustomerAsync();
        Task<bool> AddToRoleAsync(string userId, string roleName);
        Task<string?> GetCustomerRoleAsync(string userId);
    }
}