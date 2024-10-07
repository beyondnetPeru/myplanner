using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName
{
    public class ChangePlanNameCommandHandler : AbstractCommandHandler<ChangePlanNameCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangePlanNameCommandHandler(IPlanRepository planRepository, ILogger<ChangePlanNameCommandHandler> logger):base(logger)
        {
            this.planRepository = planRepository;
        }

        public override async Task<ResultSet> HandleCommand(ChangePlanNameCommand request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            planRepository.ChangeName(request.PlanId, request.Name);

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Name for plan wrong. Errors: {plan.GetBrokenRules().ToString()}");
            }

            return ResultSet.Success($"Plan name updated sucessfully");
        }
    }
}
