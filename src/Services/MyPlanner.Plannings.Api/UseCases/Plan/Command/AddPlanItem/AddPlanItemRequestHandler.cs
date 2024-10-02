using MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.AddPlanItem
{
    public class AddPlanItemRequestHandler : IRequestHandler<CreatePlanItemRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<CreatePlanItemRequest> logger;

        public AddPlanItemRequestHandler(IPlanRepository planRepository, ILogger<CreatePlanItemRequest> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreatePlanItemRequest request, CancellationToken cancellationToken)
        {
            var planItem = PlanItem.Create(IdValueObject.Create(),
                                                IdValueObject.Create(request.PlanId),
                                                IdValueObject.Create(request.ProductId),
                                                IdValueObject.Create(request.PlanCategoryId),
                                                BusinessFeature.Create(request.BusinessFeatureName, request.BusinessFeatureDefinition, request.BusinessFeatureComplexityLevel, request.BusinessFeaturePriority, request.BusinessFeatureMoScoW),
                                                TechnicalDefinition.Create(request.TechnicalDefinition),
                                                ComponentsImpacted.Create(request.ComponentsImpacted),
                                                TechnicalDependencies.Create(request.TechnicalDependencies),
                                                IdValueObject.Create(request.SizeModelTypeItemId),
                                                BallParkCost.Create(request.BallParkCostSymbol.Id, request.BallParkCostAmount, request.BallparkDependenciesCostAmount),
                                                KeyAssumptions.Create(request.KeyAssumptions),
                                                UserId.Create(request.UserId));


            if (!planItem.IsValid())
            {
                logger.LogError($"PlanItem is not valid. Errros:{planItem.GetBrokenRules()}");
                return false;
            }

            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.AddPlanItem(planItem, UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                logger.LogError($"Plan is not valid. Errros:{plan.GetBrokenRules()}");
                return false;
            }

            planRepository.AddItem(planItem);

            await planRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
