using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
  public class OrderDto
  {
    public int Id { get; set; }
    [Display(Name = "Customer Id")]
    public string CustomerId { get; set; }
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
    [Display(Name = "Order Submitted Date")]
    public DateTime? OrderSubmittedDate { get; set; }
    [Display(Name = "Order Confirmed Date")]
    public DateTime? OrderConfirmedDate { get; set; }
    [Display(Name = "Total Cost")]
    public decimal? TotalCost { get; set; } = 0M;
    public string? Description { get; set; }
    [Display(Name = "Order Status Id")]
    public int OrderStatusId { get; set; } = 1; // initial order status: Pending
    public OrderStatus? OrderStatus { get; set; }
    [Display(Name = "Order Is Submitted")]
    public bool IsSubmitted { get; set; } = false;
  }
}