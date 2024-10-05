﻿using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlan
{
    public class DeactivatePlanRequestHandler : IRequestHandler<DeactivatePlanRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ILogger<DeactivatePlanRequestHandler> logger;

        public DeactivatePlanRequestHandler(IPlanRepository planRepository, ILogger<DeactivatePlanRequestHandler> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> Handle(DeactivatePlanRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.Deactivate(UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                logger.LogError($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
                return false;
            }

            planRepository.ChangeStatus(request.PlanId, PlanStatus.Inactive.Id);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return true;
        }
    }
}