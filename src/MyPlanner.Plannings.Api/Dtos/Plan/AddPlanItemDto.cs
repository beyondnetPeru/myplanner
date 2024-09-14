namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class AddPlanItemDto
    {
        public string PlanId { get; set; }
        public string CategoryName { get; internal set; }
        public string Name { get; private set; }
        public string BusinessDefinition { get; private set; }
        public string ComplexityLevel { get; private set; }
        public string BacklogName { get; private set; }
        public int Priority { get; private set; }
        public string MoScoW { get; set; }
        public string SizeModelTypeFactorId { get; set; }
        public string SizeModelTypeValueSelected { get; set; }
        public string BusinessFeature { get; set; }
        public string TechnicalDefinition { get; set; }
        public string ComponentsImpacted { get; set; }
        public string TechnicalDependencies { get; set; }
        public double BallParkCost { get; set; }
        public double BallParkDependenciesCost { get; set; }
        public string KeyAssumptions { get; set; }
        public string UserId { get; set; }
    }
}
