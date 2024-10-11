using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlanItem
{
    public class ActivatePlanItemCommandHandler : AbstractCommandHandler<ActivatePlanItemCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ActivatePlanItemCommandHandler(IPlanRepository planRepository, ILogger<AbstractCommandHandler<ActivatePlanItemCommand, ResultSet>> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(ActivatePlanItemCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.Activate(UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Plan item is not valid: {planItem.GetBrokenRules().ToString()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class ActivatePlanIdentifiedRequestHandler : IdentifiedCommandHandler<ActivatePlanItemCommand, ResultSet>
    {
        public ActivatePlanIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<ActivatePlanItemCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
