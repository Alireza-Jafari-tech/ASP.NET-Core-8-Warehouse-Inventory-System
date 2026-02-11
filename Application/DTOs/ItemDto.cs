using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ItemDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        [Display(Name = "Item Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<ItemRelationDto>? RelatedItems { get; set; }
        public ICollection<ItemRelationDto>? RelatedToItems { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        [Display(Name = "Quantity Availabe")]
        public int QuantityAvailabe { get; set; }
        [Required]
        [Display(Name = "Supplier Id")]
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        [Required]
        [Display(Name = "Status Id")]
        public int StatusId { get; set; }
        public ItemStatus? Status { get; set; }
        [Required]
        [Display(Name = "Picture Url")]
        public string PictureUrl { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Last Update")]
        public DateTime? LastUpdate { get; set; }
        [Display(Name = "Reorder Level")]
        public int? ReorderLevel { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
    }
}