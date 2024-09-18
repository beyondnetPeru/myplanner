
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeOwner
{
    public class ChangePlanOwnerRequestHandler : IRequestHandler<ChangePlanOwnerRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ILogger<ChangePlanOwnerRequest> logger;

        public ChangePlanOwnerRequestHandler(IPlanRepository planRepository, ILogger<ChangePlanOwnerRequest> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangePlanOwnerRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.ChangeOwner(Owner.Create(request.Owner), UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                logger.LogError($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
                return false;
            }

            planRepository.ChangeOwner(request.PlanId, request.Owner);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return true;

        }
    }
}
