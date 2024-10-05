using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlan
{
    public class ClosePlanRequestHandler : AbstractCommandHandler<ClosePlanRequest, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ClosePlanRequestHandler(IPlanRepository planRepository, ILogger<ClosePlanRequestHandler> logger):base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(ClosePlanRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.Close(UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Plan is not valid. Errors: {plan.GetBrokenRules().ToString()}");
            }

            planRepository.ChangeStatus(request.PlanId, PlanStatus.Closed.Id);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success("Plan closed successfully.");
        }
    }
}
