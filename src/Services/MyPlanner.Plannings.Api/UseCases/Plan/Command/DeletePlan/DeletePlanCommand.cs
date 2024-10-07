using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeletePlan
{
    public class DeletePlanCommand : ICommand<ResultSet>
    {
        public string PlanId { get; }
        public string UserId { get; }

        public DeletePlanCommand(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
