using MyPlanner.Plannings.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelTable
    {
        public string Id { get; set; }
        public string SizeModelTypeId { get; set; }
        public SizeModelTypeTable SizeModelType { get; set; }
        public string Name { get; set; }
        public ICollection<SizeModelItemTable> SizeModelItems { get; set; }
        public AuditTable Audit { get; set; }
        public int Status { get; set; }
    }

}
