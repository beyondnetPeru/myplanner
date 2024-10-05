using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlan
{
    public class DeactivatePlanRequest : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }
        public DeactivatePlanRequest(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
