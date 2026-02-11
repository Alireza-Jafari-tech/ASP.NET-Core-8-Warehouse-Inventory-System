using Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Interfaces;
using Domain.Entities;
using Application.DTOs;
using AutoMapper;

namespace Infrastructure.Services
{
	public class OrderStatusService : IOrderStatusService
	{
		private readonly IRepository<OrderStatus> _orderStatusRepository;
		private readonly IMapper _mapper;

		public OrderStatusService(IRepository<OrderStatus> orderStatusRepository, IMapper mapper)
		{
			_orderStatusRepository = orderStatusRepository;
			_mapper = mapper;
		}

		public async Task<List<OrderStatusDto>> GetAllOrdersStatusAsync()
		{
			var allOrderStatus = await _orderStatusRepository.GetAllAsync();
			return _mapper.Map<List<OrderStatusDto>>(allOrderStatus);
		}

		public async Task<OrderStatusDto> GetOrderStatusById(int id)
		{
			var orderStatus = await _orderStatusRepository.GetByIdAsync(id);
			return _mapper.Map<OrderStatusDto>(orderStatus);
		}
	}
}