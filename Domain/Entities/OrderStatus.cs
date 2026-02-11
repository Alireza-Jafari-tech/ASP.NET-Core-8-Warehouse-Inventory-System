
namespace Domain.Entities
{
  public class OrderStatus
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string CssClassName { get;  set; }
    public string? Description { get; set; }
  }
}