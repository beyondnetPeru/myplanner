using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan
{
    public class CreatePlanCommand : ICommand<ResultSet>
    {
        public string Name { get; set; } = default!;
        public ICollection<CreatePlanCategoryCommand> Categories { get; set; } = default!;
        public string Owner { get; set; } = default!;
        public string SizeModelTypeId { get; set; } = default!;
        public ICollection<CreatePlanItemCommand> Items { get; set; } = default!;
        public string UserId { get; set; } = default!;

        public CreatePlanCommand(string name, 
                                 ICollection<CreatePlanCategoryCommand> categories,
                                 string owner, 
                                 string sizeModelTypeId,
                                 ICollection<CreatePlanItemCommand> items,
                                 string userid)
        {
            Name = name;
            Categories = categories;
            Owner = owner;
            SizeModelTypeId = sizeModelTypeId;
            Items = items;
            UserId = userid;
        }
    }

    public class CreatePlanCategoryCommand : ICommand<ResultSet>
    {
        public string Name { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }

    public class CreatePlanItemCommand : ICommand<ResultSet>
    {
        public string PlanId { get; set; } = default!;
        public string ProductId { get; set; } = default!;
        public string PlanCategoryId { get; set; } = default!;
        public string BusinessFeatureName { get; private set; } = default!;
        public string BusinessFeatureDefinition { get; private set; } = default!;
        public string BusinessFeatureComplexityLevel { get; private set; } = default!;
        public int BusinessFeaturePriority { get; private set; } = default!;
        public string BusinessFeatureMoScoW { get; private set; } = default!;
        public string TechnicalDefinition { get; set; } = default!;
        public string ComponentsImpacted { get; set; } = default!;
        public string TechnicalDependencies { get; set; } = default!;
        public string SizeModelTypeItemId { get; set; } = default!;
        public int BallParkCostSymbol { get; set; } = 1;
        public double BallParkCostAmount { get; set; } = 0.00;
        public double BallparkDependenciesCostAmount { get; set; } = 0.00;
        public double BallParkTotalCostAmount { get; set; } = 0.00;
        public string KeyAssumptions { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}
