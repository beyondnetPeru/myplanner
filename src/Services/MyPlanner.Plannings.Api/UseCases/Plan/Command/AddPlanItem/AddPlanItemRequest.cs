using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan
{ 
    public class AddPlanItemRequest : ICommand<ResultSet>
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
        public CurrencySymbolEnum BallParkCostSymbol { get; set; } = CurrencySymbolEnum.USD;
        public double BallParkCostAmount { get; set; } = 0.00;
        public CurrencySymbolEnum BallparkDependenciesCostSymbol { get; set; } = CurrencySymbolEnum.USD;
        public double BallparkDependenciesCostAmount { get; set; } = 0.00;
        public CurrencySymbolEnum BallParkTotalCostSymbol { get; set; } = CurrencySymbolEnum.USD;
        public double BallParkTotalCostAmount { get; set; } = 0.00;
        public string KeyAssumptions { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}
