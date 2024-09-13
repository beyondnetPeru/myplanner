namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class PlanItemDto
    {
        public string Id { get; set; }
        public string BusinessFeature { get; set; }
        public string TechnicalDefinition { get; set; }
        public string ComponentsImpacted { get; set; }
        public string TechnicalDependencies { get; set; }
        public string SizeModelTypeFactorCode { get; set; }
        public string SizeModelTypeValueSelected { get; set; }
        public double BallParkCost { get; set; }
        public double BallParkDependenciesCost { get; set; }
        public double BallParkTotalCost { get; set; }
        public string KeyAssumptions { get; set; }
        public string Status { get; set; }
    }
}
