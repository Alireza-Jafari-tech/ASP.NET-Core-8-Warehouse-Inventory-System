using Microsoft.AspNetCore.Identity;
using Application.Interfaces;
using Application.Common.Exceptions;
using Infrastructure.Data;
using Application.DTOs;

namespace Infrastructure.Identity
{
    public class AdminService : IAdminService
    {
        private readonly IIdentityService _identityService;
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(
            IIdentityService identityService,
            AppDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _identityService = identityService;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateAdminAsync(CreateUserRequest userRequest)
        {
            // Create user first (IsAdmin flag optional because role controls access)
            var (userId, createError) =
                await _identityService.CreateUserAsync(userRequest);

            if (userId is null)
                throw new OperationFailedException(createError ?? "Could not create admin.");

            // Ensure role exists
            if (!await _identityService.RoleExistsAsync("Admin"))
                throw new NotFoundException("Admin role does not exist!");

            // Add to role
            await _identityService.AddToRoleAsync(userId, "Admin");

            return true;
        }

        public async Task<bool> SignInAdminAsync(string email, string password, bool rememberMe)
            => await _identityService.SignInAsync(email, password, rememberMe);

        public async Task SignOutAdminAsync()
            => await _identityService.SignOutAsync();

        public async Task<bool> AddToRoleAsync(string userId, string roleName)
            => await _identityService.AddToRoleAsync(userId, roleName);

        public async Task<string?> GetAdminRoleAsync(string userId)
            => await _identityService.GetUserRoleAsync(userId);
    }
}