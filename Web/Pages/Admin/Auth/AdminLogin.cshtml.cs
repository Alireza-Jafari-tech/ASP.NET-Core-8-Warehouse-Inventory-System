using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.DTOs;
using Application.Interfaces;

namespace Web.Pages.Admin.Auth
{
    public class AdminLoginModel : PageModel
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminLoginModel> _logger;
        [BindProperty]
        public AdminLoginDto Admin { get; set; }

        public AdminLoginModel(IAdminService adminService, ILogger<AdminLoginModel> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var isSuccessful = await _adminService.SignInAdminAsync(
                Admin.Email,
                Admin.Password,
                Admin.RememberMe);

            if (isSuccessful)
                return RedirectToPage("/Admin/Index");

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return Page();
        }

    }
}