namespace MyPlanner.Plannings.Models.Plan
{
    public class AddPlanItemDto : AbstractUserDto
    {
        public string ProductId { get; set; } = default!;
        public string PlanCategoryName { get; set; } = default!;
        public string BusinessFeatureName { get; set; } = default!;
        public string BusinessFeatureDefinition { get; set; } = default!;
        public int BusinessFeatureComplexityLevel { get; set; } = default!;
        public int BusinessFeaturePriority { get; set; } = default!;
        public int BusinessFeatureMoScoW { get; set; } = default!;
        public string TechnicalDefinition { get; set; } = default!;
        public string ComponentsImpacted { get; set; } = default!;
        public string TechnicalDependencies { get; set; } = default!;
        public string SizeModelTypeItemId { get; set; } = default!;
        public int BallParkCostSymbol { get; set; } = default!;
        public double BallParkCostAmount { get; set; } = 0.00;
        public double BallParkDependenciesCostAmount { get; set; } = 0.00;
        public double BallParkTotalCostAmount { get; set; } = 0.00;
        public string KeyAssumptions { get; set; } = default!;
        
        public AddPlanItemDto(string userId) : base(userId)
        {
            
        }
    }
}
