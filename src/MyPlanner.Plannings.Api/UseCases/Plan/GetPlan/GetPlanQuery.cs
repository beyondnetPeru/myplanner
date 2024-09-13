using MediatR;
using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.GetPlan
{
    public class GetPlanQuery : IRequest<PlanDto>
    {
        public GetPlanQuery(string planId)
        {
            PlanId = planId;
        }

        public string PlanId { get; }
    }
}
