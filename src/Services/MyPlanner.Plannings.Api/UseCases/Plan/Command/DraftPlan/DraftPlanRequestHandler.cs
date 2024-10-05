using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DraftPlan
{
    public class DraftPlanRequestHandler : AbstractCommandHandler<DraftPlanRequest, ResultSet>
    {
        private readonly IPlanRepository planRepository;
        
        public DraftPlanRequestHandler(IPlanRepository planRepository, ILogger<DraftPlanRequestHandler> logger) : base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(DraftPlanRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.Draft(UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Plan is not valid. Error: {plan.GetBrokenRules().ToString()}");
            }

            planRepository.ChangeStatus(request.PlanId, PlanStatus.Draft.Id);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success("Plan is drafted successfully.");
        }
    }
}
