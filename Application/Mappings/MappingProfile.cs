using AutoMapper;
using Domain.Entities;
using Application.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Supplier, SupplierDto>().ReverseMap();
        CreateMap<SupplierStatus, SupplierStatusDto>().ReverseMap();
        CreateMap<Item, ItemDto>().ReverseMap();
        CreateMap<ItemStatus, ItemStatusDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<ItemRelation, ItemRelationDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();
        CreateMap<WarehouseLog, WarehouseLogDto>().ReverseMap();
        // Add more mappings...
    }
}