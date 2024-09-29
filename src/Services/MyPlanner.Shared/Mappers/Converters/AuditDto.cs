namespace MyPlanner.Shared.Mappers.Converters
{
    public class AuditDto
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string TimeSpan { get; set; }
    }
}
