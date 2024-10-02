using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlan
{
    public class ActivatePlanRequestHandler : IRequestHandler<ActivatePlanRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ILogger<ActivatePlanRequestHandler> logger;

        public ActivatePlanRequestHandler(IPlanRepository planRepository, ILogger<ActivatePlanRequestHandler> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ActivatePlanRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            if (!plan.IsValid())
            {
                logger.LogError($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
                return false;
            }

            plan.Activate(UserId.Create(request.UserId));

            planRepository.ChangeStatus(request.PlanId, PlanStatus.Active.Id);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return true;
        }
    }
}
