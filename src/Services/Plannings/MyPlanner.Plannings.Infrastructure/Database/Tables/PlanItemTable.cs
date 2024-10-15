using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class PlanItemTable
    {
        [Key]
        public string Id { get; set; }
        public int PlanItemType { get; set; }
        public string PlanId { get; set; }
        public PlanTable Plan { get; set; }
        [ForeignKey("ProductId")]
        public string ProductId { get; set; }
        [ForeignKey("PlanCategoryId")]
        public string PlanCategoryId { get; set; }
        public string BusinessFeatureName { get; set; }
        public string BusinessFeatureDefinition { get; set; }
        public int BusinessFeatureComplexityLevel { get; set; }
        public int BusinessFeaturePriority { get; set; }
        public int BusinessFeatureMoScoW { get;set; }
        public string TechnicalDefinition { get; set; }
        public string ComponentsImpacted { get; set; }
        public string TechnicalDependencies { get; set; }
        [ForeignKey("SizeModelTypeItemId")]
        public string SizeModelTypeItemId { get; set; }
        public int BallParkCostSymbol { get; set; }
        public double BallParkCostAmount { get; set; } = 0.00;
        public double BallparkDependenciesCostAmount { get; set; } = 0.00;
        public double BallParkTotalCostAmount { get; set; } = 0.00;
        public string KeyAssumptions { get; set; }
        public AuditTable Audit { get; set; }
        public int Status { get; set; }
    }
}
