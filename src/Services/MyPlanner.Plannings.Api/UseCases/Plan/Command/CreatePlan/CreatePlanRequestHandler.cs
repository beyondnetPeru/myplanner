using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan
{
    public class CreatePlanRequestHandler : IRequestHandler<CreatePlanRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ILogger<CreatePlanRequestHandler> logger;

        public CreatePlanRequestHandler(IPlanRepository planRepository, ILogger<CreatePlanRequestHandler> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreatePlanRequest request, CancellationToken cancellationToken)
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
                logger.LogError($"Invalid plan. Errors: {plan.GetBrokenRules()}");
                return false;
            }

            foreach (var item in request.Categories)
            {
                var planCategory = PlanCategory.Create(IdValueObject.Create(), planId, Name.Create(item.Name));

                if (!planCategory.IsValid())
                {
                    logger.LogError($"Invalid plan category. Errors: {planCategory.GetBrokenRules()}");
                    return false;
                }                
            }
           

            foreach (var i in request.Items)
            {
                var planItem = PlanItem.Create(IdValueObject.Create(),
                                               IdValueObject.Create(i.PlanId),
                                               IdValueObject.Create(i.ProductId),
                                               IdValueObject.Create(i.PlanCategoryId),
                                               BusinessFeature.Create(i.BusinessFeatureName, i.BusinessFeatureDefinition, i.BusinessFeatureComplexityLevel, i.BusinessFeaturePriority, i.BusinessFeatureMoScoW),
                                               TechnicalDefinition.Create(i.TechnicalDefinition),
                                               ComponentsImpacted.Create(i.ComponentsImpacted),
                                               TechnicalDependencies.Create(i.TechnicalDependencies),
                                               IdValueObject.Create(i.SizeModelTypeItemId),
                                               BallParkCost.Create(i.BallParkCostSymbol.Id, i.BallParkCostAmount, i.BallparkDependenciesCostAmount),
                                               KeyAssumptions.Create(i.KeyAssumptions),
                                               userId);
                if (!planItem.IsValid())
                {
                    logger.LogError($"Invalid plan item. Errors: {planItem.GetBrokenRules()}");
                    return false;
                }

                plan.AddPlanItem(planItem, userId);
            }

            planRepository.Create(plan);

            await planRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;
        }
    }
}
