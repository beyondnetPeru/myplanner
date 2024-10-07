using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlan
{
    public class DeactivatePlanCommandHandler : AbstractCommandHandler<DeactivatePlanCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public DeactivatePlanCommandHandler(IPlanRepository planRepository, ILogger<DeactivatePlanCommandHandler> logger) : base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(DeactivatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.Deactivate(UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
            }

            planRepository.ChangeStatus(request.PlanId, PlanStatus.Inactive.Id);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success("Plan deactivated successfully.");
        }
    }
}
