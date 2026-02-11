using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class SupplierStatusDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(400)]
        public string? Description { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Css Class Name")]
        public string CssClassName { get; set; }
    }
}