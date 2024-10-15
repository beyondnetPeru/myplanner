using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelItemTable
    {
        public string Id { get; set; }
        [ForeignKey("SizeModelId")]
        public string SizeModelId { get; set; }
        public SizeModelTable SizeModel { get; set; }

        [ForeignKey("SizeModelTypeItem")]
        public string SizeModelTypeItemId { get; set; }
        public SizeModelTypeItemTable SizeModelTypeItem { get; set; }
        public string SizeModelTypeItemCode { get; set; }
        public int FactorSelected { get; set; }
        public string ProfileName { get; set; }
        public int ProfileAvgRateSymbol { get; set; }
        public double ProfileAvgRateValue { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public bool IsStandard { get; set; }
        public int Status { get; set; }
        public AuditTable Audit { get; set; }
    }
}
