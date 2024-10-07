using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlan
{
    public class ClosePlanCommand : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }

        public ClosePlanCommand(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
