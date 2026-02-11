using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.DTOs;
using Application.Interfaces;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Customer.Catalogs
{
    [Authorize(Roles = "Customer")]
    public class ViewItemModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IOrderItemService _orderItemService;
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ItemDto Item { get; private set; }
        public List<ItemDto> RelatedItems { get; private set; } = new();
        public string CustomerId { get; set; }
        public OrderDto? CustomerOrder { get; set; }
        public bool ItemExistsInCart { get; set; } = false;

        [BindProperty]
        public int Quantity { get; set; }

        public ViewItemModel(
            ICatalogService catalogService,
            IOrderItemService orderItemService,
            IOrderService orderService,
            IItemService itemService,
            UserManager<ApplicationUser> userManager)
        {
            _catalogService = catalogService;
            _orderItemService = orderItemService;
            _orderService = orderService;
            _itemService = itemService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await LoadItemAsync(id);

            var user = await _userManager.GetUserAsync(User);
            CustomerId = await _userManager.GetUserIdAsync(user);
            if (string.IsNullOrEmpty(CustomerId))
                throw new NotFoundException("Customer not found!");
            CustomerOrder = await _orderService.GetOrderByCustomerId(CustomerId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                await LoadItemAsync(id);
                return Page();
            }

            await LoadItemAsync(id);

            var user = await _userManager.GetUserAsync(User);
            CustomerId = await _userManager.GetUserIdAsync(user);


            if (string.IsNullOrEmpty(CustomerId))
                throw new NotFoundException("Customer not found!");

            CustomerOrder = await _orderService.GetOrderByCustomerId(CustomerId);

            if (CustomerOrder == null)
            {
                // _orderService.CreateCustomerOrder() return the dto with updated Id
                CustomerOrder = await _orderService.CreateCustomerOrderAsync(user.Id, user.CompanyName);
            }

            var exists = await _orderItemService.OrderItemExistsAsync(Item.Id, CustomerOrder.Id);

            if (exists)
            {
                await _orderItemService.UpdateOrderItemAsync(CustomerOrder.Id, Item.Id, Quantity);
            }
            else
            {
                var newOrderItem = new OrderItemDto()
                {
                    ItemId = Item.Id,
                    OrderId = CustomerOrder.Id,
                    Quantity = Quantity
                    
                };

                await _orderItemService.AddOrderItemAsync(newOrderItem);
            }

            return RedirectToPage();
        }

        public async Task LoadItemAsync(int id)
        {
            Item = await _catalogService.GetItemByIdAsync(id)
                   ?? throw new NotFoundException("Item not found!");

            RelatedItems.Clear();

            if (Item.RelatedItems == null)
                return;

            foreach (var relation in Item.RelatedItems)
            {
                var relatedItem = await _catalogService.GetItemByIdAsync(relation.RelatedItemId);
                if (relatedItem != null)
                    RelatedItems.Add(relatedItem);
            }
        }
    }
}