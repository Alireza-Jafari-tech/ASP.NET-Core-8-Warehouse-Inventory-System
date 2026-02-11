using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IItemRelationService
    {
        Task AddItemRelationsAsync(List<ItemRelationDto> itemRelationDtoList);
        Task<List<ItemRelationDto>> GetRelatedItemsAsync(int id);
        Task<bool> ItemRelationExistsAsync(ItemRelation itemRelation);
    }
}