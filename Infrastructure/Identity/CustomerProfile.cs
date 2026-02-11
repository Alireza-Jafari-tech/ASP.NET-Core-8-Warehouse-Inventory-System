using Domain.Entities;

namespace Infrastructure.Identity
{
  public class CustomerProfile
  {
    public int Id { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser? User { get; set; }
    public string CompanyName { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
  }
}