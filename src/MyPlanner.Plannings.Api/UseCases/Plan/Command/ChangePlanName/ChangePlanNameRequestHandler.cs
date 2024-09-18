using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName
{
    public class ChangePlanNameRequestHandler : IRequestHandler<ChangePlanNameRequest, bool>
    {
        private readonly IPlanRepository planRepository;

        public ChangePlanNameRequestHandler(IPlanRepository planRepository)
        {
            this.planRepository = planRepository;
        }

        public async Task<bool> Handle(ChangePlanNameRequest request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            planRepository.ChangeName(request.PlanId, request.Name);

            return true;
        }
    }
}
