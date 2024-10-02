using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelTable
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("SizeModelTypeId")]
        public string SizeModelTypeId { get; set; }
        public string SizeModelTypeCode { get; set; }
        public string Name { get; set; }
        public ICollection<SizeModelItemTable> Items { get; set; }
        public AuditTable Audit { get; set; }
        public int Status { get; set; }
    }

}
