namespace Domain.Entities
{
  public class OrderItem
  {
    public int ItemId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }

    public Item? Item { get; set; }
    public Order? Order { get; set; }
  }
}