using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.RemovePlanItem
{
    public class RemovePlanItemCommandHandler : AbstractCommandHandler<RemovePlanItemCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public RemovePlanItemCommandHandler(IPlanRepository planRepository, ILogger<RemovePlanItemCommandHandler> logger) : base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(RemovePlanItemCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.Delete(UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Error removing plan item. Errors: {planItem.GetBrokenRules().ToString()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class RemovePlanItemIdentifiedRequestHandler : IdentifiedCommandHandler<RemovePlanItemCommand, ResultSet>
    {
        public RemovePlanItemIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<RemovePlanItemCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }

    }
}
