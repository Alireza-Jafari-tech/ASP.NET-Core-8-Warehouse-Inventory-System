using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.DTOs;
using Application.Interfaces;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Admin.Items
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IItemService _itemService;
        private readonly IItemRelationService _itemRelationService;
        private readonly ISupplierService _supplierService;
        private readonly ICategoryService _categoryService;
        private readonly IItemStatusService _itemStatusService;
        private readonly IWarehouseLogService _warehouseLogService;

        public IndexModel(
        IItemService itemService,
        IItemRelationService itemRelationService,
        ISupplierService supplierService,
        ICategoryService categoryService,
        IItemStatusService itemStatusService,
        IWarehouseLogService warehouseLogService)
        {
            _itemService = itemService;
            _itemRelationService = itemRelationService;
            _supplierService = supplierService;
            _categoryService = categoryService;
            _itemStatusService = itemStatusService;
            _warehouseLogService = warehouseLogService;
        }

        public IEnumerable<ItemDto> Items { get; set; }
        [BindProperty]
        public ItemDto ItemDto { get; set; }
        [BindProperty]
        public ItemFilterDto ItemFilterDto { get; set; }
        [BindProperty]
        public List<ItemRelationDto> RelatedItems { get; set; }
        [BindProperty]
        public List<SelectListItem> CatergoriesDropdown { get; set; }
        public List<SelectListItem> SuppliersDropdown { get; set; }
        public List<SelectListItem> ItemsStatusDropdown { get; set; }
        public decimal MaxPrice { get; set; }

        public async Task OnGetAsync()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            SuppliersDropdown = suppliers.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var categories = await _categoryService.GetAllCategoriesAsync();
            CatergoriesDropdown = categories.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var itemsStatus = await _itemStatusService.GetAllItemStatusAsync();
            ItemsStatusDropdown = itemsStatus.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            Items = await _itemService.GetAllItemsAsync();

            MaxPrice = Items.Max(m => m.Cost);
        }

        public async Task<IActionResult> OnPostAddOrUpdateItemAsync()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            SuppliersDropdown = suppliers.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var categories = await _categoryService.GetAllCategoriesAsync();
            CatergoriesDropdown = categories.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var itemsStatus = await _itemStatusService.GetAllItemStatusAsync();
            ItemsStatusDropdown = itemsStatus.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            // Logic to handle form submission can be added here
            if (!ModelState.IsValid)
                return Page();

            if (ItemDto.Id == 0)
            {
                await _itemService.AddItemAsync(ItemDto);

                var newWarehouseLog = new WarehouseLogDto()
                {
                    Title = "New item was added to warehouse",
                    IconClass = "fas fa-box-circle-check",
                    Details = "New item was added to warehouse"
                };

                await _warehouseLogService.AddWarehouseLogAsync(newWarehouseLog);
            }
            else
            {
                ItemDto.RelatedItems = RelatedItems;
                await _itemService.UpdateItemAsync(ItemDto, ItemDto.Id);

                var newWarehouseLog = new WarehouseLogDto()
                {
                    Title = "An item in warehouse was updated",
                    IconClass = "fas fa-calculator",
                    Details = "An item in warehouse was updated"
                };

                await _warehouseLogService.AddWarehouseLogAsync(newWarehouseLog);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostFilterItemsAsync()
        {
            // get back OnGet data
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            SuppliersDropdown = suppliers.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var categories = await _categoryService.GetAllCategoriesAsync();
            CatergoriesDropdown = categories.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var itemsStatus = await _itemStatusService.GetAllItemStatusAsync();
            ItemsStatusDropdown = itemsStatus.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            // Logic to handle form submission can be added here
            ModelState.Clear();

            if (!TryValidateModel(ItemFilterDto, nameof(ItemFilterDto)))
                return Page();

            Items = await _itemService.GetAllFilteredItems(ItemFilterDto);
            return Page();
        }
    }
}