using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

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
}
