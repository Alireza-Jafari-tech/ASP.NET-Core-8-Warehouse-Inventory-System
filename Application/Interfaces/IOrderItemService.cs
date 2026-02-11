using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync();
        Task AddOrderItemAsync(OrderItemDto orderItemDto);
        Task<bool> ItemExistsInCartAsync(int orderId, int itemId);
        Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<int> GetOrderItemQuantityAsync(int orderId, int itemId);
        int GetOrderItemsCount();
        Task<bool> OrderItemExistsAsync(int itemId, int orderId);
        Task<bool> UpdateOrderItemAsync(int orderId, int itemId, int updatedQuantity);
        IQueryable<OrderItem> Query();
    }
}