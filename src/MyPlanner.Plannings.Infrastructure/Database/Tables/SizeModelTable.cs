using MyPlanner.Plannings.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelTable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<SizeModelItemTable> Items { get; set; }
        public AuditTable Audit { get; set; }
        public int Status { get; set; }
    }

}
