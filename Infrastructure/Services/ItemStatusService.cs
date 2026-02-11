using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
using AutoMapper;
using Application.DTOs;

namespace Infrastructure.Services
{
    public class ItemStatusService : IItemStatusService
    {
        private readonly IRepository<ItemStatus> _itemStatusRepository;
        private readonly IMapper _mapper;
        public ItemStatusService(IRepository<ItemStatus> itemStatusRepository, IMapper mapper)
        {
            _itemStatusRepository = itemStatusRepository;
            _mapper = mapper;
        }

        public async Task CreateItemStatusAsync(ItemStatusDto itemStatusDto)
        {
            var newItemStatus = _mapper.Map<ItemStatus>(itemStatusDto);
            await _itemStatusRepository.AddAsync(newItemStatus);
        }

        public async Task<IEnumerable<ItemStatusDto>> GetAllItemStatusAsync()
        {
            var itemsStatus = await _itemStatusRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemStatusDto>>(itemsStatus);
        }

        public async Task<ItemStatusDto> GetItemStatusByIdAsync(int id)
        {
            var itemStatus = await _itemStatusRepository.GetByIdAsync(id);
            return _mapper.Map<ItemStatusDto>(itemStatus);
        }
    }
}