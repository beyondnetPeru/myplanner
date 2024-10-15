using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan
{
    public class CreatePlanCommand : ICommand<ResultSet>
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ICollection<CreatePlanCategoryCommand> Categories { get; set; }
        public string Owner { get; set; } = default!;
        public string SizeModelTypeId { get; set; } = default!;
        public ICollection<CreatePlanItemCommand> Items { get; set; }
        public string UserId { get; set; } = default!;

        public CreatePlanCommand()
        {
            Categories = new List<CreatePlanCategoryCommand>();
            Items = new List<CreatePlanItemCommand>();
        }
    }

    public class CreatePlanCategoryCommand : ICommand<ResultSet>
    {
        public string Name { get; set; } = default!;
    }

    public class CreatePlanItemCommand : ICommand<ResultSet>
    {
        public string ProductId { get; set; } = default!;
        public int PlanItemType { get; set; } = default!;
        public string PlanCategoryName { get; set; } = default!;
        public string BusinessFeatureName { get; set; } = default!;
        public string BusinessFeatureDefinition { get; set; } = default!;
        public ComplexityLevelEnum BusinessFeatureComplexityLevel { get; set; } = ComplexityLevelEnum.Low;
        public PriorityOrder BusinessFeaturePriority { get; set; } = default!;
        public MoScoWEnum BusinessFeatureMoScoW { get; set; } = MoScoWEnum.NiceToHave;
        public string TechnicalDefinition { get; set; } = default!;
        public string ComponentsImpacted { get; set; } = default!;
        public string TechnicalDependencies { get; set; } = default!;
        public string SizeModelTypeItemId { get; set; } = default!;
        public CurrencySymbolEnum BallParkCostSymbol { get; set; } = CurrencySymbolEnum.USD;
        public double BallParkCostAmount { get; set; } = 0.00;
        public double BallparkDependenciesCostAmount { get; set; } = 0.00;
        public double BallParkTotalCostAmount { get; set; } = 0.00;
        public string KeyAssumptions { get; set; } = default!;
        public string UserId { get; set; } = default!;

        
    }
}
