using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.Common.Exceptions;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IItemService _itemService;
        private readonly ISupplierService _supplierService;
        private readonly IOrderItemService _orderItemService;
        private readonly IWarehouseLogService _warehouseLogService;

        public IndexModel(
            IItemService itemService,
            IOrderItemService orderItemService,
            ISupplierService supplierService,
            IWarehouseLogService warehouseLogService)
        {
            _itemService = itemService;
            _orderItemService = orderItemService;
            _supplierService = supplierService;
            _warehouseLogService = warehouseLogService;
        }

        public List<SelectListItem> ItemsDropdown { get; set; }
        public List<SelectListItem> SuppliersDropdown { get; set; }
        public IEnumerable<WarehouseLogDto> Activities { get; set; }
        public IEnumerable<ItemDto> StockAlerts { get; set; }
        [BindProperty]
        public ShipmentDto NewShipmentDto { get; set; }
        [BindProperty]
        public AdjustDto AdjustItemDto { get; set; }

        // dashboard statistics
        public int TotalItems { get; set; }
        public decimal TotalValue { get; set; }
        public int LowStockAlerts { get; set; }
        public int ActiveSuppliers { get; set; }

        public async Task OnGetAsync()
        {
            var items = await _itemService.GetAllItemsAsync();
            ItemsDropdown = items.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var suppliers = await _supplierService.GetAllSuppliersAsync();
            SuppliersDropdown = suppliers.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            Activities = await _warehouseLogService.GetAllWarehouseLogsAsync();
            var lowStockLevel = 40;
            StockAlerts = await _itemService.GetLowStockItemsAsync(lowStockLevel);

            TotalItems = _itemService.GetItemsCount();
            TotalValue = _itemService.GetItemsTotalValue();
            LowStockAlerts = _itemService.GetLowStockItemsCount(lowStockLevel);
            ActiveSuppliers = _supplierService.GetActiveSuppliersCount();
        }

        public async Task<IActionResult> OnPostReceiveShipmentAsync()
        {
            var items = await _itemService.GetAllItemsAsync();
            ItemsDropdown = items.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var suppliers = await _supplierService.GetAllSuppliersAsync();
            SuppliersDropdown = suppliers.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            Activities = await _warehouseLogService.GetAllWarehouseLogsAsync();
            var lowStockLevel = 40;
            StockAlerts = await _itemService.GetLowStockItemsAsync(lowStockLevel);

            ModelState.Clear();

            if (!TryValidateModel(NewShipmentDto, nameof(NewShipmentDto)))
                return Page();

            var item = await _itemService.GetItemByIdAsync(NewShipmentDto.ItemId);
            if (item == null)
                throw new NotFoundException("item not found!");

            item.QuantityAvailabe += NewShipmentDto.ReceivedQuantity;

            await _itemService.UpdateItemQuantityAsync(item, item.Id);

            var newWarehouseLog = new WarehouseLogDto()
            {
                Title = "New shipment was received",
                IconClass = "fas fa-shipping-fast",
                Details = "New shipment was received"
            };

            await _warehouseLogService.AddWarehouseLogAsync(newWarehouseLog);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAdjustItemAsync()
        {
            var items = await _itemService.GetAllItemsAsync();

            ItemsDropdown = items.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var suppliers = await _supplierService.GetAllSuppliersAsync();
            SuppliersDropdown = suppliers.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            Activities = await _warehouseLogService.GetAllWarehouseLogsAsync();
            var lowStockLevel = 40;
            StockAlerts = await _itemService.GetLowStockItemsAsync(lowStockLevel);

            ModelState.Clear();

            if (!TryValidateModel(NewShipmentDto, nameof(NewShipmentDto)))
                return Page();

            var item = await _itemService.GetItemByIdAsync(AdjustItemDto.ItemId);
            if (item == null)
                throw new NotFoundException("item not found!");

            var newWarehouseLogDto = new WarehouseLogDto();

            switch (AdjustItemDto.AdjustmentType)
            {
                case "increase":
                    item.QuantityAvailabe += AdjustItemDto.Quantity;

                    newWarehouseLogDto = new WarehouseLogDto()
                    {
                        Title = $"Quantity for item with Id:{item.Id} was increased",
                        IconClass = "fas fa-shipping-fast",
                        Details = "Increasement in Stock"
                    };
                    await _warehouseLogService.AddWarehouseLogAsync(newWarehouseLogDto);
                    break;
                case "decrease":
                    item.QuantityAvailabe -= AdjustItemDto.Quantity;

                    newWarehouseLogDto = new WarehouseLogDto()
                    {
                        Title = $"Quantity for item with Id:{item.Id} was decreased",
                        IconClass = "fas fa-arrow-down",
                        Details = "Decreasment in Stock"
                    };
                    await _warehouseLogService.AddWarehouseLogAsync(newWarehouseLogDto);
                    break;
                case "Set Exact Quantity":
                    item.QuantityAvailabe = AdjustItemDto.Quantity;

                    newWarehouseLogDto = new WarehouseLogDto()
                    {
                        Title = $"Quantity of item with Id:{item.Id} was adjusted",
                        IconClass = "fas fa-sliders-h",
                        Details = "Adjustment in item quantity"
                    };
                    await _warehouseLogService.AddWarehouseLogAsync(newWarehouseLogDto);
                    break;
                default:
                    // what do i write here?
                    break;
            }

            await _itemService.UpdateItemQuantityAsync(item, item.Id);
            // await _warehouseLogService.AddWarehouseLogAsync(newWarehouseLogDto);

            return RedirectToPage();
        }
    }

    public class ShipmentDto
    {
        public int ItemId { get; set; }
        public int ReceivedQuantity { get; set; }
        public int SupplierId { get; set; }
        public string? Notes { get; set; }
    }

    public class AdjustDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string AdjustmentType { get; set; }
        public string ReasonForAdjustment { get; set; }
        public string? Notes { get; set; }
    }
}