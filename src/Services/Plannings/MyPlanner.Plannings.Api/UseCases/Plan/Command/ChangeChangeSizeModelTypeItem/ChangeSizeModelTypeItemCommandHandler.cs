using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeChangeSizeModelTypeItem
{
    public class ChangeSizeModelTypeItemCommandHandler : AbstractCommandHandler<ChangeSizeModelTypeItemCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangeSizeModelTypeItemCommandHandler(IPlanRepository planRepository, ILogger<ChangeSizeModelTypeItemCommandHandler> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(ChangeSizeModelTypeItemCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.ChangeSizeModelTypeItemId(IdValueObject.Create(request.SizeModelTypeItemId), UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error(
                    $"Error changing size model type item with id {request.SizeModelTypeItemId} for plan item with id {request.PlanItemId}. Errors: {planItem.GetBrokenRules().ToString()}");
            }
            
            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class ChangeSizeModelTypeItemIdentifiedRequestHandler : IdentifiedCommandHandler<ChangeSizeModelTypeItemCommand, ResultSet>
    {
        public ChangeSizeModelTypeItemIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<ChangeSizeModelTypeItemCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
