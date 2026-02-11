using Application.DTOs;
using Domain.Interfaces;
using Domain.Entities;
using Application.Interfaces;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Infrastructure.Data;
using AutoMapper;

namespace Infrastructure.Services
{
	public class OrderService : IOrderService
	{
		private readonly IRepository<Order> _orderRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWarehouseLogService _warehouseLogService;
		private readonly IOrderItemRepository _orderItemService;
		private readonly IMapper _mapper;

		public OrderService(
			IRepository<Order> orderRepository,
      		UserManager<ApplicationUser> userManager,
			IWarehouseLogService warehouseLogService,
			IOrderItemRepository orderItemService,
			IMapper mapper)
		{
			_orderRepository = orderRepository;
			_userManager = userManager;
			_warehouseLogService = warehouseLogService;
			_orderItemService = orderItemService;
			_mapper = mapper;
		}

		public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
		{
			var query = _orderRepository.Query();
			query = query
			.Include(i => i.OrderStatus)
			.Where(c => c.IsSubmitted == true);

			var orders = await query.ToListAsync();
			return _mapper.Map<IEnumerable<OrderDto>>(orders);
		}

		public async Task<IEnumerable<OrderDto>> GetRecentOrdersAsync()
		{
			var query = _orderRepository.Query();
			query = query
			.Include(i => i.OrderItems)
			.Include(s => s.OrderStatus)
			.Where(c => c.OrderStatusId == 2); // confirmed order status id = 2

			var orders = await query.ToListAsync();

			return _mapper.Map<IEnumerable<OrderDto>>(orders);
		}

		public async Task<OrderDto> GetOrderByIdAsync(int id)
		{
			var query = _orderRepository.Query();
			query = query
			.Include(os => os.OrderStatus)
			.Include(o => o.OrderItems)
				.ThenInclude(i => i.Item);

			var order = await query.SingleOrDefaultAsync(c => c.Id == id);

			if (order == null)
				throw new NotFoundException("order not found!");

			return _mapper.Map<OrderDto>(order);
		}

		public async Task<OrderDto?> GetOrderByCustomerId(string customerId)
		{
			var query = _orderRepository.Query();
			var order = await query.FirstOrDefaultAsync(c => c.CustomerId == customerId);

			if (order == null)
				return null;

			return _mapper.Map<OrderDto>(order);
		}

		public async Task<OrderDto> AddOrderAsync(OrderDto orderDto)
		{
			var newOrder = _mapper.Map<Order>(orderDto);
			await _orderRepository.AddAsync(newOrder);

			orderDto.Id = newOrder.Id;

			return orderDto;
		}

		public async Task<OrderDto> CreateCustomerOrderAsync(string? customerId, string? compnayName)
		{
			var newCustomerOrder = new OrderDto { CustomerId = customerId, CompanyName = compnayName };
			var newOrderDto = await AddOrderAsync(newCustomerOrder);

			return newOrderDto;
		}

		public async Task<bool> OrderExists(int orderId)
		{
			var query = _orderRepository.Query();

			return await query.AnyAsync(c => c.Id == orderId);
		}

		public async Task SubmitOrderAsync(int orderId)
		{
			var order = await _orderRepository.GetByIdAsync(orderId);
			if (order == null)
				throw new NotFoundException("order doesnt exist!");

			order.IsSubmitted = true;
			order.OrderSubmittedDate = DateTime.Now;
			order.OrderStatusId = 1; // Submitted status: Pending

			await _orderRepository.UpdateAsync(order);
		}

		public async Task UpdateOrderAsync(OrderDto dto, int id)
		{
			var existing = await _orderRepository.GetByIdAsync(id);
			if (existing == null)
				throw new NotFoundException("order not found!");

			_mapper.Map(dto, existing);
			await _orderRepository.UpdateAsync(existing);
		}

		public async Task UpdateOrderStatusAsync(int orderId, int newOrderStatus)
		{
			var order = await _orderRepository.GetByIdAsync(orderId);
			if (order == null)
				throw new NotFoundException("order not found!");

			order.OrderStatusId = newOrderStatus;

		var orderDto = _mapper.Map<OrderDto>(order);

			await UpdateOrderAsync(orderDto, orderId);
		}

		public async Task ConfirmOrderAsync(int orderId, decimal orderTotalCost)
		{
			var order = await GetOrderByIdAsync(orderId);
			if (order == null)
				throw new NotFoundException("order not found!");

			// loop on all of its items
            foreach (var orderItem in order.OrderItems)
            {
                // subtract the ordered quantity from the item's available quantity
                orderItem.Item.QuantityAvailabe -= orderItem.Quantity;

                await _orderItemService.UpdateOrderItemQuantityAsync(
                	orderItem.OrderId,
                	orderItem.ItemId,
                	0); // 0 means that order item is now nothing and shouldn't appear in user's next carts
            }

            order.OrderConfirmedDate = DateTime.Now;
            order.IsSubmitted = false;
            order.TotalCost = orderTotalCost;
            await UpdateOrderAsync(order, order.Id);

            var newWarehouseLog = new WarehouseLogDto()
            {
                Title = "Order was confirmed and items are taken from warehouse",
                Details = "1 order was confirmed and all demanded items were taken from warehouse",
                IconClass = "fas fa-cash-register"
            };

            await _warehouseLogService.AddWarehouseLogAsync(newWarehouseLog);
		}

		public async Task RemoveOrderAsync(int id)
		{
			await _orderRepository.RemoveAsync(id);
		}

		public IQueryable<Order> OrderQuery()
		{
			return _orderRepository.Query();
		}
	}
}