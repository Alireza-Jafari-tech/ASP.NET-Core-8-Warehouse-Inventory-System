using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<ItemDto> GetItemByIdAsync(int id);
        Task<IEnumerable<ItemDto>> GetAllFilteredItems(ItemFilterDto filterDto);
    }
}