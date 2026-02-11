using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs
{
    public class ItemRelationDto
    {
        [Required]
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        [Required]
        public int RelatedItemId { get; set; }
        public Item? RelatedItemEntity { get; set; }
        [Required]
        [Display(Name = "Relation Type Id")]
        public int RelationTypeId { get; set; }
    }
}