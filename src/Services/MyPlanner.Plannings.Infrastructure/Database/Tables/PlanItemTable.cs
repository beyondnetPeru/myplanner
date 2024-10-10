using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class PlanItemTable
    {
        [Key]
        public string Id { get; set; } = default!;
        public string PlanId { get; set; } = default!;
        public PlanTable Plan { get; set; } = default!;
        [ForeignKey("ProductId")]
        public string ProductId { get; set; } = default!;
        [ForeignKey("PlanCategoryId")]
        public string PlanCategoryId { get; set; } = default!;
        public string BusinessFeatureName { get; set; } = default!;
        public string BusinessFeatureDefinition { get; set; } = default!;
        public int BusinessFeatureComplexityLevel { get; set; } = default!;
        public int BusinessFeaturePriority { get; set; } = default!;
        public int BusinessFeatureMoScoW { get;set; } = default!;
        public string TechnicalDefinition { get; set; } = default!;
        public string ComponentsImpacted { get; set; } = default!;
        public string TechnicalDependencies { get; set; } = default!;
        [ForeignKey("SizeModelTypeItemId")]
        public string SizeModelTypeItemId { get; set; } = default!;
        public int BallParkCostSymbol { get; set; } = default!;
        public double BallParkCostAmount { get; set; } = 0.00;
        public double BallparkDependenciesCostAmount { get; set; } = 0.00;
        public double BallParkTotalCostAmount { get; set; } = 0.00;
        public string KeyAssumptions { get; set; } = default!;
        public AuditTable Audit { get; set; } = default!;
        public int Status { get; set; } = default!;
    }
}
