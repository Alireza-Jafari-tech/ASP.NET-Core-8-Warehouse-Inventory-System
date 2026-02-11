using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
	public interface IWarehouseLogService
	{
		Task AddWarehouseLogAsync(WarehouseLogDto warehouseLogDto);
		Task<IEnumerable<WarehouseLogDto>> GetAllWarehouseLogsAsync();
	}
}