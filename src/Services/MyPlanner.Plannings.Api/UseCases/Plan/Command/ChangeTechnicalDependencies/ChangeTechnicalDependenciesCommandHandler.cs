using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeTechnicalDependencies
{
    public class ChangeTechnicalDependenciesCommandHandler : AbstractCommandHandler<ClosePlanItemCommand, ResultSet>
    {
        private readonly IPlanRepository repository;

        public ChangeTechnicalDependenciesCommandHandler(IPlanRepository repository, ILogger<AbstractCommandHandler<ClosePlanItemCommand, ResultSet>> Logger) : base(Logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async override Task<ResultSet> HandleCommand(ClosePlanItemCommand request, CancellationToken cancellationToken)
        {
            var planItem = await repository.GetItemById(request.PlanItemId);

            planItem.ChangeTechnicalDependencies(TechnicalDependencies.Create(request.TechnicalDependencies), UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Plan item with id {request.PlanItemId} is not valid. Errors: {planItem.GetBrokenRules().ToString()}");
            }

            repository.UpdateItem(planItem);

            await repository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class ChangeTechnicalDependenciesIdentifiedRequestHandler : IdentifiedCommandHandler<ClosePlanItemCommand, ResultSet>
    {
        public ChangeTechnicalDependenciesIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<ClosePlanItemCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
