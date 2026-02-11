using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces;

namespace Web.Identity.Account.Admin
{
  public class SignOutAdminModel : PageModel
  {
    private readonly IAdminService _adminService;
    public SignOutAdminModel(IAdminService adminService)
    {
      _adminService = adminService;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
      await _adminService.SignOutAdminAsync();
      return RedirectToPage("/Admin/Auth/AdminLogin");
    }
  }
}