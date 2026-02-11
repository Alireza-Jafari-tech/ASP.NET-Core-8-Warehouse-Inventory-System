using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreateUserRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
        [Required]
        public bool IsAdmin { get; set; } = false;
        [Display(Name = "Full name")]
        public string? FullName { get; set; }
        [Display(Name = "Company name")]
        public string? CompanyName { get; set; }
    }
}