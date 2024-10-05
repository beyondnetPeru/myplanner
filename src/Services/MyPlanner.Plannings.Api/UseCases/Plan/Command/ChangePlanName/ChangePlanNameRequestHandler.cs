using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName
{
    public class ChangePlanNameRequestHandler : AbstractCommandHandler<ChangePlanNameRequest, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangePlanNameRequestHandler(IPlanRepository planRepository, ILogger<ChangePlanNameRequestHandler> logger):base(logger)
        {
            this.planRepository = planRepository;
        }

        public override async Task<ResultSet> HandleCommand(ChangePlanNameRequest request, CancellationToken cancellationToken)
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
