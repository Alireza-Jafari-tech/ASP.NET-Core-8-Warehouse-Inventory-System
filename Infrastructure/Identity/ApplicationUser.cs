using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? CompanyName { get; set; }
        public bool IsAdmin { get; set; }
    }
}