using AutoMapper;
using Domain.Entities;
using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using AutoMapper;

namespace Infrastructure.Services
{
	public class WarehouseLogService : IWarehouseLogService
	{
		private readonly IRepository<WarehouseLog> _warehouseLogRepository;
		private readonly IMapper _mapper;

		public WarehouseLogService(
			IRepository<WarehouseLog> warehouseLogRepository,
			IMapper mapper)
		{
			_warehouseLogRepository = warehouseLogRepository;
			_mapper = mapper;
		}

		public async Task AddWarehouseLogAsync(WarehouseLogDto warehouseLogDto)
		{
			var newWarehouseLogEntity = _mapper.Map<WarehouseLog>(warehouseLogDto);
			await _warehouseLogRepository.AddAsync(newWarehouseLogEntity);
		}

		public async Task<IEnumerable<WarehouseLogDto>> GetAllWarehouseLogsAsync()
		{
			var warehouseLogs = await _warehouseLogRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<WarehouseLogDto>>(warehouseLogs);
		}
	}
}