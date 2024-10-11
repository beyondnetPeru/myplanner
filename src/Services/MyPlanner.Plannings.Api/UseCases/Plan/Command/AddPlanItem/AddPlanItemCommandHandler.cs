using MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.AddPlanItem
{
    public class AddPlanItemCommandHandler : AbstractCommandHandler<AddPlanItemCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public AddPlanItemCommandHandler(IPlanRepository planRepository, ILogger<AddPlanItemCommandHandler> logger):base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(AddPlanItemCommand request, CancellationToken cancellationToken)
        {
            var planCategory = await planRepository.GetCategoryByName(request.PlanId,request.PlanCategoryName);

            var planItem = PlanItem.Create(IdValueObject.Create(),
                                           IdValueObject.Create(request.PlanId),
                                           IdValueObject.Create(request.ProductId),
                                           planCategory.GetPropsCopy().Id,
                                           BusinessFeature.Create(request.BusinessFeatureName, 
                                                                  request.BusinessFeatureDefinition, 
                                                                  Enumeration.FromValue<ComplexityLevelEnum>(request.BusinessFeatureComplexityLevel), 
                                                                  request.BusinessFeaturePriority, 
                                                                  Enumeration.FromValue<MoScoWEnum>(request.BusinessFeatureMoScoW)),
                                           TechnicalDefinition.Create(request.TechnicalDefinition),
                                           ComponentsImpacted.Create(request.ComponentsImpacted),
                                           TechnicalDependencies.Create(request.TechnicalDependencies),
                                           IdValueObject.Create(request.SizeModelTypeItemId),
                                           BallParkCost.Create(Enumeration.FromValue<CurrencySymbolEnum>(request.BallParkCostSymbol), request.BallParkCostAmount, request.BallParkDependenciesCostAmount),
                                           KeyAssumptions.Create(request.KeyAssumptions),
                                           UserId.Create(request.UserId));


            if (!planItem.IsValid())
            {
                return ResultSet.Error($"PlanItem is not valid. Errros:{planItem.GetBrokenRules().ToString()}");                
            }

            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.AddPlanItem(planItem, UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Plan is not valid. Errros:{plan.GetBrokenRules().ToString()}");
            }

            planRepository.AddItem(planItem);

            await planRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return ResultSet.Success();
        }
    }

    public class AddPlanIdentifiedRequestHandler : IdentifiedCommandHandler<AddPlanItemCommand, ResultSet>
    {
        public AddPlanIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<AddPlanItemCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
