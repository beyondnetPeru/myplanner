using MyPlanner.Plannings.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class PlanTable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string SizeModelTypeId { get; set; }
        public ICollection<PlanItemTable> Items { get; set; }
        public AuditTable Audit { get; set; }
        public int Status { get; set; }
    }
}
