using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan
{
    public class CreatePlanCommandHandler : AbstractCommandHandler<CreatePlanCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public CreatePlanCommandHandler(IPlanRepository planRepository, ILogger<CreatePlanCommandHandler> logger) : base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(request.UserId);

            var planId = IdValueObject.Create();

            var plan = Domain.PlanAggregate.Plan.Create(planId,
                                                        PlanCode.Create(request.Code),
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

                plan.AddCategory(planCategory, userId);
            }

            foreach (var i in request.Items)
            {
                var planItem = PlanItem.Create(IdValueObject.Create(),
                                               Enumeration.FromValue<PlanItemTypeEnum>(i.PlanItemType)!,
                                               planId,
                                               IdValueObject.Create(i.ProductId),
                                               IdValueObject.Create(plan.GetCategoryIdByName(i.PlanCategoryName)),
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

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class CreatePlanIdentifiedRequestHandler : IdentifiedCommandHandler<CreatePlanCommand, ResultSet>
    {
        public CreatePlanIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CreatePlanCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
