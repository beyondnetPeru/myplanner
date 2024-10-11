using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlanItem
{
    public class ClosePlanItemCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public string UserId { get; set; }

        public ClosePlanItemCommand(string planItemId, string userId)
        {
            PlanItemId = planItemId;
            UserId = userId;
        }
    }
}
