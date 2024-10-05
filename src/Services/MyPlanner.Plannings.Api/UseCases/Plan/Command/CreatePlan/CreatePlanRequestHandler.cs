using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan
{
    public class CreatePlanRequestHandler : AbstractCommandHandler<CreatePlanRequest, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public CreatePlanRequestHandler(IPlanRepository planRepository, ILogger<CreatePlanRequestHandler> logger) : base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(CreatePlanRequest request, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(request.UserId);

            var planId = IdValueObject.Create();

            var plan = Domain.PlanAggregate.Plan.Create(planId,
                                                        IdValueObject.Create(request.SizeModelTypeId),
                                                        Name.Create(request.Name),
                                                        Owner.Create(request.Owner),
                                                        userId);
            if (!plan.IsValid())
            {
                return ResultSet.Error($"Invalid plan. Errors: {plan.GetBrokenRules()}");                
            }

            foreach (var item in request.Categories)
            {
                var planCategory = PlanCategory.Create(IdValueObject.Create(), planId, Name.Create(item.Name));

                if (!planCategory.IsValid())
                {
                    return ResultSet.Error($"Invalid plan category. Errors: {planCategory.GetBrokenRules()}");
                }                
            }

            foreach (var i in request.Items)
            {
                var planItem = PlanItem.Create(IdValueObject.Create(),
                                               planId,
                                               IdValueObject.Create(i.ProductId),
                                               IdValueObject.Create(i.PlanCategoryId),
                                               BusinessFeature.Create(i.BusinessFeatureName, i.BusinessFeatureDefinition, i.BusinessFeatureComplexityLevel, i.BusinessFeaturePriority, i.BusinessFeatureMoScoW),
                                               TechnicalDefinition.Create(i.TechnicalDefinition),
                                               ComponentsImpacted.Create(i.ComponentsImpacted),
                                               TechnicalDependencies.Create(i.TechnicalDependencies),
                                               IdValueObject.Create(i.SizeModelTypeItemId),
                                               BallParkCost.Create(i.BallParkCostSymbol, i.BallParkCostAmount, i.BallparkDependenciesCostAmount),
                                               KeyAssumptions.Create(i.KeyAssumptions),
                                               userId);
                if (!planItem.IsValid())
                {
                    return ResultSet.Error($"Invalid plan item. Errors: {planItem.GetBrokenRules()}");
                }

                plan.AddPlanItem(planItem, userId);
            }

            planRepository.Create(plan);

            await planRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return ResultSet.Success("Plan created successfully.");
        }
    }
}
