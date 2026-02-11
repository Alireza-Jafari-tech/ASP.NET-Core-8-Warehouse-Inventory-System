using Application.DTOs;
using Domain.Interfaces;
using Domain.Entities;
using Application.Interfaces;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using AutoMapper;

namespace Infrastructure.Services
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ItemService(
            IRepository<Item> itemRepository,
            AppDbContext context,
            IMapper mapper)
        {
            _itemRepository = itemRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public async Task<ItemDto> GetItemByIdAsync(int id)
        {
            var query = _itemRepository.Query();
            var item = query
              .Include(i => i.Category)
              .Include(s => s.Status)
              .Include(r => r.RelatedItems)
              .FirstOrDefault(c => c.Id == id);

            if (item == null)
                throw new NotFoundException("item not found!");

            return _mapper.Map<ItemDto>(item);
        }

        public async Task<IEnumerable<ItemDto>> GetLowStockItemsAsync(int lowStockLevel)
        {
            var query = _itemRepository.Query();

            query = query
            .Include(i => i.Status)
            .Where(c => c.QuantityAvailabe <= lowStockLevel);

            var lowStockItems = await query.ToListAsync();

            return _mapper.Map<IEnumerable<ItemDto>>(lowStockItems);
        }

        public int GetLowStockItemsCount(int lowStockLevel)
        {
            var query = _itemRepository.Query();

            query = query
                .Where(c => c.QuantityAvailabe <= lowStockLevel);

            return query.Count();
        }

        public int GetItemsCount()
        {
            var query = _itemRepository.Query();

            return query.Count();
        }

        public decimal GetItemsTotalValue()
        {
            var query = _itemRepository.Query();

            return query.Sum(i => i.QuantityAvailabe * i.Cost);
        }

        public async Task AddItemAsync(ItemDto itemDto)
        {
            var newItem = _mapper.Map<Item>(itemDto);
            await _itemRepository.AddAsync(newItem);
        }

        public async Task UpdateItemAsync(ItemDto dto, int id)
        {
            var existing = await _itemRepository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException("item not found!");

            _mapper.Map(dto, existing);

            await _itemRepository.UpdateAsync(existing);
        }

        public async Task UpdateItemQuantityAsync(ItemDto dto, int id)
        {
            var existing = await _itemRepository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException("item not found!");

            existing.QuantityAvailabe = dto.QuantityAvailabe;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(int id)
        {
            var existing = await _itemRepository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException("item not found!");

            await _itemRepository.RemoveAsync(id);
        }

        public async Task<IEnumerable<ItemDto>> GetAllFilteredItems(ItemFilterDto filterDto)
        {
            var query = _itemRepository.Query();

            if (!string.IsNullOrWhiteSpace(filterDto.SearchItemString))
                query = query.Where(c =>
                  c.Name.Contains(filterDto.SearchItemString) ||
                  c.Description.Contains(filterDto.SearchItemString));
            if (filterDto.CategoryId != 0)
                query = query.Where(c => c.CategoryId == filterDto.CategoryId);
            if (filterDto.StatusId != 0)
                query = query.Where(s => s.StatusId == filterDto.StatusId);
            if (filterDto.IsAvailable == true)
                query = query.Where(a => a.QuantityAvailabe > 0);
            if (filterDto.MinCost != null && filterDto.MinCost > 0)
                query = query.Where(s => s.Cost > filterDto.MinCost || s.Cost == filterDto.MinCost);
            if (filterDto.MaxCost != null && filterDto.MaxCost > 0)
                query = query.Where(s => s.Cost < filterDto.MaxCost || s.Cost == filterDto.MaxCost);

            var filteredItems = await query
            .Include(i => i.Category)
            .Include(s => s.Status)
            .ToListAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(filteredItems);
        }

        public async Task<ItemDto> GetRelatedItemAsync(int id)
        {
            var item = _itemRepository.Query()
              .Include(i => i.RelatedItems)
                .FirstOrDefault(c => c.Id == id);

            return _mapper.Map<ItemDto>(item);
        }
    }
}