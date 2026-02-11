using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.DTOs;
using Application.Interfaces;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Admin.Items
{
  [Authorize(Roles = "Admin")]
  public class AddRelationModel : PageModel
  {
    private readonly IItemRelationService _itemRelationService;
    private readonly IItemService _itemService;
    public IEnumerable<ItemDto> AllItems { get; set; }
    public ItemDto EditingItem { get; set; }
    public List<ItemRelationDto> ItemRelations { get; set; }
    [BindProperty]
    public List<ItemRelationDto> ItemRelationDtoList { get; set; }
    public AddRelationModel(IItemService itemService, IItemRelationService itemRelationService)
    {
      _itemRelationService = itemRelationService;
      _itemService = itemService;
    }

    public async Task<IActionResult> OnGet(int id)
    {
      AllItems = await _itemService.GetAllItemsAsync();

      EditingItem = await GetItemById(id);
      if (EditingItem == null)
        throw new NotFoundException("item not found!");

      ItemRelations = EditingItem.RelatedItems;

      return Page();
    }

    public async Task<IActionResult> OnPost()
    {
      if (!ModelState.IsValid)
        return Page();

      await _itemRelationService.AddItemRelationsAsync(ItemRelationDtoList);
      return RedirectToPage();
    }
    public async Task<ItemDto> GetItemById(int id)
    {
      var item = await _itemService.GetRelatedItemAsync(id);
      if (item == null)
        throw new NotFoundException("item not found!");

      return item;
    }

    public class RelationTypeVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public List<RelationTypeVm> RelationTypes { get; } = new()
    {
        new RelationTypeVm { Id = 1, Name = "Accessory" },
        new RelationTypeVm { Id = 2, Name = "Upgrade" },
        new RelationTypeVm { Id = 3, Name = "Variant" },
        new RelationTypeVm { Id = 4, Name = "Substitute" },
        new RelationTypeVm { Id = 5, Name = "Compatible With" },
        new RelationTypeVm { Id = 6, Name = "Requires" }
    };

    public string GetRelationTypeName(int typeId)
    {
      return typeId switch
      {
        1 => "Accessory",
        2 => "Upgrade",
        3 => "Variant",
        4 => "Substitute",
        5 => "Compatible With",
        6 => "Requires",
        _ => "Unknown"
      };
    }
  }
}
