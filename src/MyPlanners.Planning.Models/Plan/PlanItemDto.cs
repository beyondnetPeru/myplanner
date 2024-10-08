namespace MyPlanner.Plannings.Models.Plan
{
    public class PlanItemDto
    {
        public string Id { get; set; } = default!;
        public string BusinessFeature { get; set; } = default!;
        public string TechnicalDefinition { get; set; } = default!; 
        public string ComponentsImpacted { get; set; } = default!;
        public string TechnicalDependencies { get; set; } = default!;
        public string SizeModelTypeFactorCode { get; set; } = default!;
        public string SizeModelTypeValueSelected { get; set; } = default!;
        public double BallParkCost { get; set; } = default!;
        public double BallParkDependenciesCost { get; set; } = default!;
        public double BallParkTotalCost { get; set; } = default!;
        public string KeyAssumptions { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
