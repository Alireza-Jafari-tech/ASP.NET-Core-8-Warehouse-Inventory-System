using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
  public class OrderItemDto
  {
    [Required]
    public int ItemId { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int Quantity { get; set; }

    public Item? Item { get; set; }
    public Order? Order { get; set; }
  }
}