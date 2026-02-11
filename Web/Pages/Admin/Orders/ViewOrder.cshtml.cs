using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Admin.Orders
{
    [Authorize(Roles = "Admin")]
    public class ViewOrderModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderStatusService _orderStatusService;
        private readonly IWarehouseLogService _warehouseLogService;
        private readonly IMapper _mapper;

        public OrderDto Order { get; set; }
        [BindProperty]
        public int OrderId { get; set; }
        [BindProperty]
        public decimal OrderTotalCost { get; set; }
        [BindProperty]
        public int SelectedOrderStatusId { get; set; }
        public List<SelectListItem> OrderStatusDropdown { get; set; }

        public ViewOrderModel(
            IOrderService orderService,
            IOrderStatusService orderStatusService,
            IWarehouseLogService warehouseLogService,
            IMapper mapper)
        {
            _orderService = orderService;
            _orderStatusService = orderStatusService;
            _warehouseLogService = warehouseLogService;
            _mapper = mapper;
        }

        public async Task OnGetAsync(int id)
        {
            Order = await _orderService.GetOrderByIdAsync(id);

            var ordersStatus = await _orderStatusService.GetAllOrdersStatusAsync();
            var orderStatusDtoList = _mapper.Map<IEnumerable<OrderStatusDto>>(ordersStatus);

            OrderStatusDropdown = orderStatusDtoList
            .Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();
        }

        // confirm customer order
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // var existingOrder = _orderService.GetOrderByIdAsync(OrderId);

            await _orderService.UpdateOrderStatusAsync(OrderId, SelectedOrderStatusId);

            // confirmed status id = 2
            // also check whether or not the order was confirmed before or not
            if (SelectedOrderStatusId == 2)
            {
                await _orderService.ConfirmOrderAsync(OrderId, OrderTotalCost);
            }

            return RedirectToPage("/Admin/Orders/Index");
        }

        public Tax GetTax()
        {
            var tax = new Tax()
            {
                Percent = 8M
            };

            return tax;
        }

        public class Tax
        {
            public decimal Percent { get; set; }
            public decimal? Cost { get; set; }
        }
    }
}