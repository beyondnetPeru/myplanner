using MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.AddPlanItem
{
    public class AddPlanItemRequestHandler : AbstractCommandHandler<CreatePlanItemRequest, ResultSet>
    {
        private readonly IPlanRepository planRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public AddPlanItemRequestHandler(IPlanRepository planRepository, ILogger<AddPlanItemRequestHandler> logger):base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        
        }

        public override async Task<ResultSet> HandleCommand(CreatePlanItemRequest request, CancellationToken cancellationToken)
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
                                                BallParkCost.Create(request.BallParkCostSymbol, request.BallParkCostAmount, request.BallparkDependenciesCostAmount),
                                                KeyAssumptions.Create(request.KeyAssumptions),
                                                UserId.Create(request.UserId));


            if (!planItem.IsValid())
            {
                return ResultSet.Error($"PlanItem is not valid. Errros:{planItem.GetBrokenRules()}");
                
            }

            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.AddPlanItem(planItem, UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Plan is not valid. Errros:{plan.GetBrokenRules()}");
                
            }

            planRepository.AddItem(planItem);

            await planRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return ResultSet.Success("Plan created sucessfully.");
        }
    }
}
