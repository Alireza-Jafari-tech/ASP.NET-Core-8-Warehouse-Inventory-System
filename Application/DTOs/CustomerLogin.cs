using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CustomerLoginDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}