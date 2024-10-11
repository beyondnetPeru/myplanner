using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DraftPlanItem
{
    public class DraftPlanItemCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public string UserId { get; set; }

        public DraftPlanItemCommand(string planItemId, string userId)
        {
            PlanItemId = planItemId;
            UserId = userId;
        }

    }
}
