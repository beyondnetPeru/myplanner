﻿using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeComponentsImpacted
{
    public class ChangeComponentsImpactedCommandHandler : AbstractCommandHandler<ChangeComponentsImpactedCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangeComponentsImpactedCommandHandler(IPlanRepository planRepository, ILogger<AbstractCommandHandler<ChangeComponentsImpactedCommand, ResultSet>> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(ChangeComponentsImpactedCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.ChangeComponentsImpacted(ComponentsImpacted.Create(request.ComponentsImpacted), UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Plan item is not valid: {planItem.GetBrokenRules().ToString()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
