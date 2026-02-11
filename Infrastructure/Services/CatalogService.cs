using Domain.Interfaces;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IRepository<Item> _catalogRepository;
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        public CatalogService(
            IRepository<Item> catalogRepository,
            IItemService itemService,
            IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _itemService = itemService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            var query = _catalogRepository.Query();
            var items = await query
              .Include(i => i.Category)
              .Include(s => s.Status)
              .Include(r => r.RelatedItems)
              .ToListAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public async Task<ItemDto> GetItemByIdAsync(int id)
        {
            return await _itemService.GetItemByIdAsync(id);
        }

        public async Task<IEnumerable<ItemDto>> GetAllFilteredItems(ItemFilterDto filterDto)
        {
            var query = _catalogRepository.Query();

            if (!string.IsNullOrWhiteSpace(filterDto.SearchItemString))
                query = query.Where(c =>
                  c.Name.Contains(filterDto.SearchItemString) ||
                  c.Description.Contains(filterDto.SearchItemString));
            if (filterDto.CategoryId != 0)
                query = query.Where(c => c.CategoryId == filterDto.CategoryId);
            if (filterDto.StatusId != 0)
                query = query.Where(c => c.StatusId == filterDto.StatusId);
            if (filterDto.IsAvailable == true)
                query = query.Where(a => a.QuantityAvailabe > 0);
            if (filterDto.MinCost != null && filterDto.MinCost > 0)
                query = query.Where(c => c.Cost > filterDto.MinCost);
            if (filterDto.MaxCost != null && filterDto.MaxCost > 0)
                query = query.Where(c => c.Cost < filterDto.MaxCost);

            var items = await query
            .Include(i => i.Category)
            .Include(s => s.Status)
            .ToListAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }
    }
}