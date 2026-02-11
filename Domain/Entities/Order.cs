namespace Domain.Entities
{
  public class Order
  {
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public string CompanyName { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
    public DateTime? OrderSubmittedDate { get; set; }
    public DateTime? OrderConfirmedDate { get; set; }
    public decimal? TotalCost { get; set; } = 0M;
    public string? Description { get; set; }
    public int OrderStatusId { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public bool IsSubmitted { get; set; } = false;
  }
}