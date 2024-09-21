using MyPlanner.Plannings.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelItemTable
    {
        public string Id { get; set; }
        public SizeModelTable SizeModel { get; set; }
        public string SizeModelId { get; set; }
        public string ProfileName { get; set; }
        public double ProfileAvgRateAmount { get; set; }
        public string SizeModelFactorSelected { get; set; }
        public string ProfileValueSelected { get; set; }
        public int ProfileCountValue { get; set; }
        public bool IsStandard { get; set; }
        public double TotalCost { get; set; }
        public AuditTable Audit { get; set; }
        public int Status { get; set; }
    }
}
