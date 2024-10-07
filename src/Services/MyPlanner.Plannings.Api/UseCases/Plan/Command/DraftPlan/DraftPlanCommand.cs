using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DraftPlan
{
    public class DraftPlanCommand : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string UserId { get; }

        public DraftPlanCommand(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
