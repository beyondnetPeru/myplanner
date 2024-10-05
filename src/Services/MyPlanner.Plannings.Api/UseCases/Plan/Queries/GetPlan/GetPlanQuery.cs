using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlan
{
    public class GetPlanQuery : IQuery<ResultSet>
    {
        public GetPlanQuery(string planId)
        {
            PlanId = planId;
        }

        public string PlanId { get; }
    }
}
