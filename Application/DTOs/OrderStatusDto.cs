using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class OrderStatusDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(600)]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "Css Class Name")]
        public string CssClassName { get; set; }
    }
}