using MyPlanner.Plannings.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelItemTable
    {
        public string Id { get; set; }
        public string SizeModelId { get; set; }
        public SizeModelTable SizeModel { get; set; }
        public string SizeModelTypeFactorId { get; set; }
        public string SizeModelTypeFactorCode { get; set; }
        public string ProfileName { get; set; }
        public double ProfileAvgRateAmount { get; set; }
        public double ProfileValueSelected { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public AuditTable Audit { get; set; }
    }
}
