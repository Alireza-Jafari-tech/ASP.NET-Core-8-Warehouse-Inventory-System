namespace Infrastructure.Identity
{
  public class AdminProfile
  {
    public int Id { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser User { get; set; }
  }
}