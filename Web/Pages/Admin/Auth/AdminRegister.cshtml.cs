using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.DTOs;
using Application.Interfaces;

namespace Web.Pages.Admin.Auth
{
    public class AdminRegisterModel : PageModel
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminRegisterModel> _logger;
        [BindProperty]
        public CreateUserRequest Admin { get; set; }

        public AdminRegisterModel(IAdminService adminService, ILogger<AdminRegisterModel> logger)
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

            Admin.IsAdmin = true;

            var isSuccessful = await _adminService.CreateAdminAsync(Admin);

            if (isSuccessful)
                return RedirectToPage("/Admin/Auth/AdminLogin");

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return Page();
        }

    }
}