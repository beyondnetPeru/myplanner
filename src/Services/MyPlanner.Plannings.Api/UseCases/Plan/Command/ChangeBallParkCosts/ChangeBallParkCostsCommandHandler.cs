using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeBallParkCosts
{
    public class ChangeBallParkCostsCommandHandler : AbstractCommandHandler<ChangeBallParkCostsCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangeBallParkCostsCommandHandler(IPlanRepository planRepository, ILogger<AbstractCommandHandler<ChangeBallParkCostsCommand, ResultSet>> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(ChangeBallParkCostsCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.ChangeBallParkCosts(BallParkCost.Create(Enumeration.FromValue<CurrencySymbolEnum>(request.CurrencySymbol), request.BallParkCost, request.BallparkDependenciesCost), UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Error updating Ball Park Costs for Plan Item {request.PlanItemId}. Errors: {planItem.GetBrokenRules().ToString()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
