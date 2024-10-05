using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlan
{
    public class ActivatePlanRequest : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }

        public ActivatePlanRequest(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
