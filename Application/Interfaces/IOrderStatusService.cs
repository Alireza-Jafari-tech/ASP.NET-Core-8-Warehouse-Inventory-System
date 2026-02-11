using Application.DTOs;

namespace Application.Interfaces
{
	public interface IOrderStatusService
	{
		Task<List<OrderStatusDto>> GetAllOrdersStatusAsync();
		Task<OrderStatusDto> GetOrderStatusById(int id);
	}
}