using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.DTOs;
using Application.Interfaces;

namespace Web.Pages.Customer.Auth
{
    public class CustomerLoginModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerLoginModel> _logger;
        [BindProperty]
        public CustomerLoginDto Customer { get; set; }

        public CustomerLoginModel(ICustomerService customerService, ILogger<CustomerLoginModel> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var isSuccessful = await _customerService.SignInCustomerAsync(
                Customer.Email,
                Customer.Password,
                Customer.RememberMe);

            if (isSuccessful)
                return RedirectToPage("/Customer/Index");

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return Page();
        }

    }
}