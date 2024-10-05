﻿using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeletePlan
{
    public class DeletePlanRequestHandler : IRequestHandler<DeletePlanRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ILogger<DeletePlanRequestHandler> logger;

        public DeletePlanRequestHandler(IPlanRepository planRepository, ILogger<DeletePlanRequestHandler> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeletePlanRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.Delete(request.PlanId, UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                logger.LogError($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
                return false;
            }

            planRepository.ChangeStatus(request.PlanId, PlanStatus.Deleted.Id);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return true;
        }
    }
}