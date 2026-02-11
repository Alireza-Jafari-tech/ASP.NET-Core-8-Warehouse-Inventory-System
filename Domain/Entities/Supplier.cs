namespace Domain.Entities
{
  public class Supplier
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactPerson { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int SupplierStatusId { get; set; }
    public SupplierStatus? SupplierStatus { get; set; }
    public ICollection<Item> SuppliedItems { get; set; } = new List<Item>();
    public DateTime MembershipDate { get; set; } = DateTime.Now;
    public string Address { get; set; }
    public string? Notes { get; set; }
  }
}