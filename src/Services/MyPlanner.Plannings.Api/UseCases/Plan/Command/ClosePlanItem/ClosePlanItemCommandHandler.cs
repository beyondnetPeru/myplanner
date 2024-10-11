using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlanItem
{
    public class ClosePlanItemCommandHandler : AbstractCommandHandler<ClosePlanItemCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ClosePlanItemCommandHandler(IPlanRepository planRepository, ILogger<AbstractCommandHandler<ClosePlanItemCommand, ResultSet>> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(ClosePlanItemCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.Close(UserId.Create(request.UserId));

            if (!planItem.IsValid()) {
                return ResultSet.Error(
                    $"Plan item is not valid. {planItem.GetBrokenRules()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
