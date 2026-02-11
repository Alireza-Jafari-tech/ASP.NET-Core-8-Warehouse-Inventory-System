using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ItemFilterDto
    {
        [Display(Name = "Search String")]
        public string? SearchItemString { get; set; } = "";
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        [Display(Name = "Status Id")]
        public int StatusId { get; set; }
        [Required]
        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }
        [Display(Name = "Min item Cost")]
        public decimal? MinCost { get; set; }
        [Display(Name = "Max item Cost")]
        public decimal? MaxCost { get; set; }
    }
}