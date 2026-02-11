using Application.DTOs;
using Domain.Interfaces;
using Domain.Entities;
using Application.Interfaces;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services
{
	public class OrderItemService : IOrderItemService
	{
		private readonly IRepository<OrderItem> _orderItemRepository;
		private readonly IOrderItemRepository _privateRepository;
		private readonly IMapper _mapper;

		public OrderItemService(
			IRepository<OrderItem> orderItemRepository,
			IOrderItemRepository privateRepository,
			IMapper mapper)
		{
			_orderItemRepository = orderItemRepository;
			_privateRepository = privateRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync()
		{
			var orderItems = await _orderItemRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
		}

        public async Task AddOrderItemAsync(OrderItemDto orderItemDto)
        {
        	var newOrderItem = _mapper.Map<OrderItem>(orderItemDto);
        	await _orderItemRepository.AddAsync(newOrderItem);
        }

        public async Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId)
        {
        	var query = _orderItemRepository.Query();

        	var orderItems = await query
        	.Include(i => i.Item)
        	.Where(c => c.OrderId == orderId)
        	.ToListAsync();

        	var orderItemDtoList = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        	return orderItemDtoList;
        }

		public async Task<int> GetOrderItemQuantityAsync(int orderId, int itemId)
		{
			var query = _orderItemRepository.Query();

			var orderItem = await query.FirstOrDefaultAsync(c => c.OrderId == orderId && c.ItemId == itemId);
			return orderItem.Quantity;
		}

		public int GetOrderItemsCount()
		{
			var query = _orderItemRepository.Query();

			query = query
			.Where(c => c.Quantity != 0);

			return query.Count();
		}

		public async Task<bool> OrderItemExistsAsync(int itemId, int orderId)
		{
			return await _privateRepository.OrderItemExistsAsync(itemId, orderId);
		}

		public async Task<bool> UpdateOrderItemAsync(int orderId, int itemId, int updatedQuantity)
		{
			var isSuccessful =
			await _privateRepository
			.UpdateOrderItemQuantityAsync(orderId, itemId, updatedQuantity);

			return isSuccessful;
		}

		public IQueryable<OrderItem> Query()
        {
        	return _orderItemRepository.Query();
        }

        public async Task<bool> ItemExistsInCartAsync(int orderId, int itemId)
        {
        	var query = _orderItemRepository.Query();

        	var orderItemExists = await query.AnyAsync(c =>
        		c.OrderId == orderId && c.ItemId == itemId && c.Quantity != 0);

        	return orderItemExists;
        }
	}
}