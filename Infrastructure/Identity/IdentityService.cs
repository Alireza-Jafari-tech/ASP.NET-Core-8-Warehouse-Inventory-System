using Microsoft.AspNetCore.Identity;
using Application.Common.Exceptions;
using Application.DTOs;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        // => await _userManager.FindByIdAsync(userId);

        public async Task<(string? userId, string? createError)> CreateUserAsync(CreateUserRequest userRequest)
        {
            // had to define it here beacause of scope issues
            var user = new ApplicationUser();

            if (userRequest.IsAdmin) // for admin
            {
                user = new ApplicationUser
                {
                    UserName = userRequest.Email,
                    Email = userRequest.Email,
                    FullName = userRequest.FullName,
                    PhoneNumber = userRequest.PhoneNumber,
                    IsAdmin = userRequest.IsAdmin
                };
            }
            else // for customer
            {
                user = new ApplicationUser
                {
                    UserName = userRequest.Email,
                    Email = userRequest.Email,
                    CompanyName = userRequest.CompanyName,
                    PhoneNumber = userRequest.PhoneNumber,
                    IsAdmin = userRequest.IsAdmin
                };
            }

            var existingUser = await _userManager.FindByNameAsync(user.UserName);
            if (existingUser != null)
            {
                // Console.WriteLine($"User {user.UserName} already exists!");
                throw new OperationFailedException("username is taken!");
            }

            var result = await _userManager.CreateAsync(user, userRequest.Password);

            if (!result.Succeeded)
                return (null, string.Join(", ", result.Errors.Select(e => e.Description)));

            return (user.Id, null);
        }


        public async Task<bool> SignInAsync(string email, string password, bool rememberMe = false)
        {
            var result = await _signInManager.PasswordSignInAsync(
                email,
                password,
                rememberMe,
                lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task SignOutAsync()
            => await _signInManager.SignOutAsync();

        public async Task<bool> AddToRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NotFoundException("User not found!");

            if (!await _roleManager.RoleExistsAsync(roleName))
                throw new NotFoundException($"Role '{roleName}' does not exist!");

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
                throw new OperationFailedException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            return true;
        }

        public async Task<string?> GetUserRoleAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NotFoundException("User not found!");

            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

        public async Task<bool> RoleExistsAsync(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }
    }
}