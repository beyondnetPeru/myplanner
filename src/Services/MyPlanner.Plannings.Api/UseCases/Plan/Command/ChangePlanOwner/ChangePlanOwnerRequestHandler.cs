using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeOwner
{
    public class ChangePlanOwnerRequestHandler : AbstractCommandHandler<ChangePlanOwnerRequest, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangePlanOwnerRequestHandler(IPlanRepository planRepository, ILogger<ChangePlanOwnerRequestHandler> logger):base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
                }

        public override async Task<ResultSet> HandleCommand(ChangePlanOwnerRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.ChangeOwner(Owner.Create(request.Owner), UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                ResultSet.Error($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
            }

            planRepository.ChangeOwner(request.PlanId, request.Owner);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success("Plan owner updated successfully.");
        }
    }
}
