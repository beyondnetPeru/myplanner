using MyPlanner.Plannings.Infrastructure.Repositories;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangePlanItemType
{
    public class ChangePlanItemTypeCommandHandler : AbstractCommandHandler<ChangePlanItemTypeCommand, ResultSet>
    {
        private readonly PlanRepository planRepository;

        public ChangePlanItemTypeCommandHandler(PlanRepository planRepository, ILogger<AbstractCommandHandler<ChangePlanItemTypeCommand, ResultSet>> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(ChangePlanItemTypeCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.ChangePlanItemType(Enumeration.FromValue<PlanItemTypeEnum>(request.PlanItemType)!,UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Plan item is not valid: {planItem.GetBrokenRulesAsString()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class ChangePlanItemTypeIdentifiedRequestHandler : IdentifiedCommandHandler<ChangePlanItemTypeCommand, ResultSet>
    {
        public ChangePlanItemTypeIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<ChangePlanItemTypeCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
