using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.DTOs;
using Application.Interfaces;

namespace Web.Pages.Customer.Auth
{
    public class CustomerRegisterModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerRegisterModel> _logger;
        [BindProperty]
        public CreateUserRequest Customer { get; set; }

        public CustomerRegisterModel(ICustomerService customerService, ILogger<CustomerRegisterModel> logger)
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

            Customer.IsAdmin = false;

            var isSuccessful = await _customerService.CreateCustomerAsync(Customer);

            if (isSuccessful)
                return RedirectToPage("/Customer/Auth/CustomerLogin");

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return Page();
        }

    }
}