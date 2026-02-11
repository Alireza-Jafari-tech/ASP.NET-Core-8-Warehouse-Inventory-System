using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class WarehouseLogDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string IconClass { get; set; }
        [MaxLength(3000)]
        public string Details { get; set; }
        public string? Notes { get; set; }
    }
}