using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class SizeModelItemDto
    {
        public string Id { get; set; }
        public string SizeModelId { get; set; }
        public string SizeModelName { get; set; }
        public string SizeModelTypeItemId { get; set; }
        public string SizeModelTypeItemCode { get; set; }
        public int FactorSelected { get; set; }
        public string ProfileName { get; set; }
        public int ProfileAvgRateSymbol { get; set; }
        public double ProfileAvgRateValue { get; set; }    
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public bool IsStandard { get; set; }
        public string Status { get; set; }
    }
}
