
namespace Domain.Entities
{
    public class WarehouseLog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string IconClass { get; set; }
        public string Details { get; set; }
        public string? Notes { get; set; }
    }
}