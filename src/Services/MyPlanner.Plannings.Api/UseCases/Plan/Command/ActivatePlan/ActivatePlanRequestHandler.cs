using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlan
{
    public class ActivatePlanRequestHandler : AbstractCommandHandler<ActivatePlanRequest, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ActivatePlanRequestHandler(IPlanRepository planRepository, ILogger<ActivatePlanRequestHandler> logger):base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(ActivatePlanRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
            }

            plan.Activate(UserId.Create(request.UserId));

            planRepository.ChangeStatus(request.PlanId, PlanStatus.Active.Id);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success("Plan activated sucessfully.");
        }
    }
}
