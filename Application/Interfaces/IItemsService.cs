using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<ItemDto> GetItemByIdAsync(int id);
        Task<IEnumerable<ItemDto>> GetLowStockItemsAsync(int lowStockLevel);
        Task AddItemAsync(ItemDto supplierDto);
        int GetItemsCount();
        int GetLowStockItemsCount(int lowStockLevel);
        decimal GetItemsTotalValue();
        Task UpdateItemAsync(ItemDto dto, int id);
        Task UpdateItemQuantityAsync(ItemDto dto, int id);
        Task RemoveItemAsync(int id);
        Task<IEnumerable<ItemDto>> GetAllFilteredItems(ItemFilterDto filterDto);
        Task<ItemDto> GetRelatedItemAsync(int id);
    }
}