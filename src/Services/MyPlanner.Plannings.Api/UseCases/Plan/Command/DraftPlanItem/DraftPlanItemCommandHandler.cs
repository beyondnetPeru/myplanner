using MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlanItem;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DraftPlanItem
{
    public class DraftPlanItemCommandHandler : AbstractCommandHandler<DraftPlanItemCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public DraftPlanItemCommandHandler(IPlanRepository planRepository, ILogger<AbstractCommandHandler<DraftPlanItemCommand, ResultSet>> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(DraftPlanItemCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.Draft(UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Plan item is not valid: {planItem.GetBrokenRules().ToString()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class DraftPlanItemIdentifiedRequestHandler : IdentifiedCommandHandler<DraftPlanItemCommand, ResultSet>
    {
        public DraftPlanItemIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<DraftPlanItemCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
