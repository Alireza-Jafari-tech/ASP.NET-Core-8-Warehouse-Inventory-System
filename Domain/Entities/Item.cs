namespace Domain.Entities
{
    public class Item
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public int CategoryId { get; set; }
      public Category? Category { get; set; }
      public ICollection<ItemRelation>? RelatedItems { get; set; }
      public ICollection<ItemRelation>? RelatedToItems { get; set; }
      public decimal Cost { get; set; }
      public int QuantityAvailabe { get; set; }
      public int SupplierId { get; set; }
      public Supplier? Supplier { get; set; }
      public int StatusId { get; set; }
      public ItemStatus? Status { get; set; }
      public string PictureUrl { get; set; }
      public DateTime CreateDate { get; set; } = DateTime.Now;
      public DateTime? LastUpdate { get; set; }
      public int? ReorderLevel { get; set; }
      public string? Description { get; set; }
      public ICollection<OrderItem>? OrderItems { get; set; }
    }
}