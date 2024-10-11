using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlanItem
{
    public class DeactivatePlanItemCommandHandler : AbstractCommandHandler<DeactivatePlanItemCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public DeactivatePlanItemCommandHandler(IPlanRepository planRepository, ILogger<AbstractCommandHandler<DeactivatePlanItemCommand, ResultSet>> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(DeactivatePlanItemCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.Deactivate(UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Plan item is not valid: {planItem.GetBrokenRules()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class DeactivatePlanItemIdentifiedRequestHandler : IdentifiedCommandHandler<DeactivatePlanItemCommand, ResultSet>
    {
        public DeactivatePlanItemIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<DeactivatePlanItemCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
