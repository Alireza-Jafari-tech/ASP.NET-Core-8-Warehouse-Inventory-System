using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Application.DTOs;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Customer.Carts
{
  [Authorize(Roles = "Customer")]
  public class IndexModel : PageModel
  {
    private readonly IOrderItemService _orderItemService;
    private readonly IOrderService _orderService;
    private readonly UserManager<ApplicationUser> _userManager;

    [BindProperty]
    public List<ItemQuantity>? ItemQuantityList { get; set; }
    public IEnumerable<OrderItemDto>? OrderItems { get; set; }
    public bool OrderSubmitted { get; set; } = false;

    public IndexModel(
      IOrderItemService orderItemService,
      IOrderService orderService,
      UserManager<ApplicationUser> userManager)
    {
      _orderItemService = orderItemService;
      _orderService = orderService;
      _userManager = userManager;
    }

    public async Task OnGetAsync()
    {
      var user = await _userManager.GetUserAsync(User);
      if (user.Id == null)
        throw new NotFoundException("user not found!");

      var customerOrder = await _orderService.GetOrderByCustomerId(user.Id);
      if (customerOrder == null)
      {
        // _orderService.CreateCustomerOrder() creates an order for customer and returns it
        customerOrder = await _orderService.CreateCustomerOrderAsync(user.Id, user.CompanyName);
      }

      OrderSubmitted = customerOrder.IsSubmitted;

      OrderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(customerOrder.Id);
      if (OrderItems == null)
        throw new NotFoundException("no order items found!");
    }

    public async Task<IActionResult> OnPostSaveCartAsync()
    {
      var user = await _userManager.GetUserAsync(User);
      var userId = await _userManager.GetUserIdAsync(user);
      if (user.Id == null)
        return Challenge();

      if (!ModelState.IsValid)
        return Page();

      var customerOrder = await _orderService.GetOrderByCustomerId(userId)
                         ?? await _orderService.CreateCustomerOrderAsync(userId, user.CompanyName);

      var orderItems = await _orderItemService
          .GetOrderItemsByOrderIdAsync(customerOrder.Id);

      if (!orderItems.Any())
        return RedirectToPage();

      foreach (var item in ItemQuantityList)
      {
        var existingItem = orderItems
            .FirstOrDefault(x => x.ItemId == item.itemId);

        if (existingItem == null)
          continue;

        if (existingItem.Quantity != item.UpdatedQuantity)
        {
          var success = await _orderItemService
              .UpdateOrderItemAsync(customerOrder.Id, item.itemId, item.UpdatedQuantity);

          if (!success)
            return Page();
        }
      }
      return RedirectToPage();
    }

    public async Task<IActionResult> OnPostSubmitOrderAsync()
    {
      var user = await _userManager.GetUserAsync(User);
      var userId = await _userManager.GetUserIdAsync(user);
      if (user.Id == null)
        return Challenge();

      if (!ModelState.IsValid)
        return Page();

      var customerOrder = await _orderService.GetOrderByCustomerId(userId)
                         ?? await _orderService.CreateCustomerOrderAsync(userId, user.CompanyName);

      await _orderService.SubmitOrderAsync(customerOrder.Id);

      OrderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(customerOrder.Id);
      if (OrderItems == null)
        throw new NotFoundException("no order items found!");

      return RedirectToPage();
    }

    public class ItemQuantity
    {
      public int itemId { get; set; }
      public int UpdatedQuantity { get; set; }
    }
  }
}