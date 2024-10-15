namespace MyPlanner.Plannings.Models.SizeModel
{
    public class SizeModelItemDto
    {
        public string Id { get; set; } = default!;
        public string SizeModelId { get; set; } = default!; 
        public string SizeModelName { get; set; } = default!; 
        public string SizeModelTypeItemId { get; set; } = default!; 
        public string SizeModelTypeItemCode { get; set; } = default!;
        public int FactorSelected { get; set; } = default!; 
        public string ProfileName { get; set; } = default!;
        public int ProfileAvgRateSymbol { get; set; } = default!;
        public double ProfileAvgRateValue { get; set; } = default!;    
        public int Quantity { get; set; } = default!;
        public double TotalCost { get; set; } = default!;
        public bool IsStandard { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
