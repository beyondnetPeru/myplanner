using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class PlanTable
    {
        [Key]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<PlanCategoryTable> Categories { get; set; }
        public string Owner { get; set; }
        [ForeignKey("SizeModelTypeId")]
        public string SizeModelTypeId { get; set; }
        public SizeModelTypeTable SizeModelType { get; set; }
        public ICollection<PlanItemTable> Items { get; set; }
        public AuditTable Audit { get; set; }
        public int Status { get; set; }
    }
}
