using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Application.Common.Exceptions;

namespace Infrastructure.Services
{
    public class ItemRelationService : IItemRelationService
    {
        private readonly IRepository<ItemRelation> _itemRelationRepository;
        private readonly IMapper _mapper;
        
        public ItemRelationService(IRepository<ItemRelation> itemRelationRepository, IMapper mapper)
        {
            _itemRelationRepository = itemRelationRepository;
            _mapper = mapper;
        }

        public async Task AddItemRelationsAsync(List<ItemRelationDto> itemRelationDtoList)
        {
            var itemRelations = _mapper.Map<List<ItemRelation>>(itemRelationDtoList);
            foreach (var itemRelation in itemRelations)
            {
                bool itemExists = await ItemRelationExistsAsync(itemRelation);
                if (!itemExists)
                    await _itemRelationRepository.AddAsync(itemRelation);
            }
        }

        public async Task<List<ItemRelationDto>> GetRelatedItemsAsync(int id)
        {
            var RelatedItems = await _itemRelationRepository.GetByIdAsync(id);

            if (RelatedItems == null)
                throw new NotFoundException("relations not found!");

            return _mapper.Map<List<ItemRelationDto>>(RelatedItems);
        }

        public async Task<bool> ItemRelationExistsAsync(ItemRelation itemRelation)
        {
            var itemRelations = await _itemRelationRepository.GetAllAsync();
            return itemRelations.Any(
            ir => ir.ItemId == itemRelation.ItemId
            && ir.RelatedItemId == itemRelation.RelatedItemId
            && ir.RelationTypeId == itemRelation.RelationTypeId);
        }
    }
}