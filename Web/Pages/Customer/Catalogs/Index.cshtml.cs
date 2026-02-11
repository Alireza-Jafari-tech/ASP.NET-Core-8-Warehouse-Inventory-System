using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Customer.Catalogs
{
  [Authorize(Roles = "Customer")]
  public class IndexModel : PageModel
  {
    private readonly ICatalogService _catalogService;
    private readonly ISupplierService _supplierService;
    private readonly IItemStatusService _itemStatusService;
    private readonly ICategoryService _categoryService;
    public IndexModel(
      ISupplierService supplierService,
      ICategoryService categoryService,
      IItemStatusService itemStatusService,
      ICatalogService catalogService)
    {
      _catalogService = catalogService;
      _supplierService = supplierService;
      _itemStatusService = itemStatusService;
      _categoryService = categoryService;
    }
    public IEnumerable<ItemDto> Items { get; set; }
    public List<SelectListItem> SuppliersDropdown { get; set; }
    public List<SelectListItem> CatergoriesDropdown { get; set; }
    public List<SelectListItem> ItemsStatusDropdown { get; set; }
    public decimal MaxPrice { get; set; } = 100000000;
    [BindProperty]
    public ItemFilterDto ItemFilterDto { get; set; }
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
      Items = await _catalogService.GetAllItemsAsync();
    }

    public async Task<IActionResult> OnPostFilterItemsAsync()
    {
      // Logic to retrieve items can be added here
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

      Items = await _catalogService.GetAllFilteredItems(ItemFilterDto);

      return Page();
    }
  }
}