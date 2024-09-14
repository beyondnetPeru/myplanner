using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DraftPlan
{
    public class DraftPlanRequestHandler : IRequestHandler<DraftPlanRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ILogger<DraftPlanRequestHandler> logger;

        public DraftPlanRequestHandler(IPlanRepository planRepository, ILogger<DraftPlanRequestHandler> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DraftPlanRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.Draft(UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                logger.LogError($"Plan is not valid. Error: {plan.GetBrokenRules().ToString()}");
                return false;
            }

            await planRepository.Draft(request.PlanId);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return true;
        }
    }
}
