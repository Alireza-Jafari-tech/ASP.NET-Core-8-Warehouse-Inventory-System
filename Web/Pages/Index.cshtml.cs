using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
  public class IndexModel : PageModel
  {
    public IndexModel()
    {
    }

    public IActionResult OnGet()
    {
      return RedirectToPage("/Customer/Auth/CustomerLogin");
    }
  }
}