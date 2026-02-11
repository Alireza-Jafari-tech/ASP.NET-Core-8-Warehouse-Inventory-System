using Application.DTOs;

namespace Application.Interfaces
{
    public interface IItemStatusService
    {
        Task CreateItemStatusAsync(ItemStatusDto itemStatusDto);
        Task<IEnumerable<ItemStatusDto>> GetAllItemStatusAsync();
        Task<ItemStatusDto> GetItemStatusByIdAsync(int id);
    }
}