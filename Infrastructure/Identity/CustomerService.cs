using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Infrastructure.Data;
using Application.Interfaces;
using Application.Common.Exceptions;
using Application.DTOs;

namespace Infrastructure.Identity
{
    public class CustomerService : ICustomerService
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        public CustomerService(
          IIdentityService identity,
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          RoleManager<IdentityRole> roleManager,
          AppDbContext context)
        {
            _identityService = identity;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> CreateCustomerAsync(CreateUserRequest userRequest)
        {
            var (userId, createError) = await _identityService.CreateUserAsync(userRequest);

            if (userId == null || createError != null)
            {
                throw new OperationFailedException("operation failed!");
            }

            var customerRoleExists = await _identityService.RoleExistsAsync("Customer");

            if (!customerRoleExists)
            {
                throw new NotFoundException("Customer role does not exist!");
            }

            await _identityService.AddToRoleAsync(userId, "Customer");

            return true;
        }

        public async Task<bool> SignInCustomerAsync(string email, string password, bool rememberMe = false)
        => await _identityService.SignInAsync(email, password, rememberMe);

        public async Task SignOutCustomerAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> AddToRoleAsync(string userId, string roleName)
        => await _identityService.AddToRoleAsync(userId, roleName);

        public async Task<string?> GetCustomerRoleAsync(string userId)
        {
            return await _identityService.GetUserRoleAsync(userId);
        }
    }
}