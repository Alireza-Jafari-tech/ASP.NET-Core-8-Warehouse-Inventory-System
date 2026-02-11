using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDto>> GetRecentOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto?> GetOrderByCustomerId(string customerId);
        Task<OrderDto> AddOrderAsync(OrderDto orderDto);
        Task<OrderDto> CreateCustomerOrderAsync(string? customerId, string? compnayName);
        Task<bool> OrderExists(int orderId);
        Task UpdateOrderAsync(OrderDto dto, int id);
        Task SubmitOrderAsync(int orderId);
        Task ConfirmOrderAsync(int orderId, decimal orderTotalCost);
        Task UpdateOrderStatusAsync(int orderId, int newOrderStatus);
        Task RemoveOrderAsync(int id);
    }
}