using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class PlanTable
    {
        [Key]
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ICollection<PlanCategoryTable> Categories { get; set; } = default!;
        public string Owner { get; set; } = default!;
        [ForeignKey("SizeModelTypeId")]
        public string SizeModelTypeId { get; set; } = default!;
        public SizeModelTypeTable SizeModelType { get; set; } = default!;
        public ICollection<PlanItemTable> Items { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public AuditTable Audit { get; set; } = default!;
        public int Status { get; set; }
    }
}
