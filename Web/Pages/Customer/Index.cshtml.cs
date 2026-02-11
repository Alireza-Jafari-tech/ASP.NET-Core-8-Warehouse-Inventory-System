using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Application.Interfaces;
using Application.DTOs;

namespace Web.Pages.Customer
{
  [Authorize(Roles = "Customer")]
  public class IndexModel : PageModel
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOrderService _orderService;

    public ApplicationUser UserData { get; set; }
    public IEnumerable<OrderDto> Orders { get; set; }

    public IndexModel(
      UserManager<ApplicationUser> userManager, IOrderService orderService)
    {
      _userManager = userManager;
      _orderService = orderService;
    }

    public async Task OnGetAsync()
    {
      UserData = await _userManager.GetUserAsync(User);
      // get order history
      Orders = await _orderService.GetRecentOrdersAsync();
    }

    public void OnPost()
    {
      // Logic to handle form submission can be added here
    }
  }
}