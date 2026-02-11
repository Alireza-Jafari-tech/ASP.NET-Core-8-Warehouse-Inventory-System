using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;

namespace Infrastructure.Data
{
	public class OrderItemRepository : IOrderItemRepository
	{
		private readonly AppDbContext _context;

		public OrderItemRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<bool> UpdateOrderItemQuantityAsync(int orderId, int itemId, int newQuantity)
		{
			var existing = await _context.OrderItems.FindAsync(orderId, itemId);
			if (existing == null)
				throw new NotFoundException("order item not found!");

			existing.Quantity = newQuantity;

			return await _context.SaveChangesAsync() >= 0;
		}

		public async Task<bool> OrderItemExistsAsync(int itemId, int orderId)
		{
			var orderItem = await _context.OrderItems.FindAsync(orderId, itemId);
			
			if (orderItem == null)
				return false;
			return true;
		}
	}
}