using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeTechnicalDependencies
{
    public class ChangeTechnicalDependenciesCommandHandler : AbstractCommandHandler<ChangeTechnicalDependenciesCommand, ResultSet>
    {
        private readonly IPlanRepository repository;

        public ChangeTechnicalDependenciesCommandHandler(IPlanRepository repository, ILogger<AbstractCommandHandler<ChangeTechnicalDependenciesCommand, ResultSet>> Logger) : base(Logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async override Task<ResultSet> HandleCommand(ChangeTechnicalDependenciesCommand request, CancellationToken cancellationToken)
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
}
