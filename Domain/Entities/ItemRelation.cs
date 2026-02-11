
namespace Domain.Entities
{
  public class ItemRelation
  {
    public int ItemId { get; set; }
    public int RelatedItemId { get; set; }
    public int RelationTypeId { get; set; }
    
    public Item? Item { get; set; }
    public Item? RelatedItemEntity { get; set; }
  }
}