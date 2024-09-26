using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlan
{
    public class ClosePlanRequestHandler : IRequestHandler<ClosePlanRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ILogger<ClosePlanRequestHandler> logger;

        public ClosePlanRequestHandler(IPlanRepository planRepository, ILogger<ClosePlanRequestHandler> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ClosePlanRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.Close(UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                logger.LogError($"Plan is not valid. Errors: {plan.GetBrokenRules().ToString()}");

                return false;
            }

            planRepository.Close(request.PlanId);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return true;
        }
    }
}
