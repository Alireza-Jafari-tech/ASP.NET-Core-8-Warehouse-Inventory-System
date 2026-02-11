using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces;

namespace Web.Identity.Account.Customer
{
  public class SignOutCustomerModel : PageModel
  {
    private readonly ICustomerService _customerService;
    public SignOutCustomerModel(ICustomerService customerService)
    {
      _customerService = customerService;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
      await _customerService.SignOutCustomerAsync();
      return RedirectToPage("/Customer/Auth/CustomerLogin");
    }
  }
}