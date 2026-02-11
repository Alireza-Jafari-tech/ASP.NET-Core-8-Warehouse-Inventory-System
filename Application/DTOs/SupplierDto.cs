using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs
{
    public class SupplierDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(350)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Display(Name = "Phone Number")]
        public ICollection<Item>? ItemsSupplied { get; set; }
        [Required]
        [Display(Name = "Supplier Status Id")]
        public int SupplierStatusId { get; set; }
        public SupplierStatus? SupplierStatus { get; set; }
        [Display(Name = "Member ship Date")]
        public DateTime MembershipDate { get; set; } = DateTime.Now;
        [Required]
        public string Address { get; set; }
        [MaxLength(1000)]
        public string? Notes { get; set; }
    }
}