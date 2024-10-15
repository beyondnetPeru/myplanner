using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeAssumptions
{
    public class ChangeAssumptionsCommandHandler : AbstractCommandHandler<ChangeAssumptionsCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangeAssumptionsCommandHandler(IPlanRepository planRepository, ILogger<AbstractCommandHandler<ChangeAssumptionsCommand, ResultSet>> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(ChangeAssumptionsCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.ChangeKeyAssumptions(KeyAssumptions.Create(request.Assumptions), UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Error updating Key Assumptions for Plan Item {request.PlanItemId}. Errors: {planItem.GetBrokenRules().ToString()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class ChangeAssumptionsIdentifiedRequestHandler : IdentifiedCommandHandler<ChangeAssumptionsCommand, ResultSet>
    {
        public ChangeAssumptionsIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<ChangeAssumptionsCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
