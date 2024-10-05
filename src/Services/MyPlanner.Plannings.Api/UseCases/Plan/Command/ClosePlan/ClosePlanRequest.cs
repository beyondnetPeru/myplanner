using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlan
{
    public class ClosePlanRequest : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }

        public ClosePlanRequest(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
