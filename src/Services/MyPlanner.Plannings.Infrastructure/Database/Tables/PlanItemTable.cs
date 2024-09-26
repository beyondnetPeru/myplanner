using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class PlanItemTable
    {
        public string Id { get; set; }
        public PlanTable Plan { get; set; }
        public string BusinessFeature { get; set; }
        public string TechnicalDefinition { get; set; }
        public string ComponentsImpacted { get; set; }
        public string TechnicalDependencies { get; set; }
        public string SizeModelTypeFactorId { get; set; }
        public string SizeModelTypeFactorCode { get; set; }
        public string SizeModelTypeValueSelected { get; set; }
        public double BallParkCost { get; set; }
        public double BallParkDependenciesCost { get; set; }
        public double BallParkTotalCost { get; set; }
        public string KeyAssumptions { get; set; }
        public AuditTable Audit { get; set; }
        public int Status { get; set; }
    }
}
