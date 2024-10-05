using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeOwner
{
    public class ChangePlanOwnerRequest : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string Owner { get; set; }
        public string UserId { get; }

        public ChangePlanOwnerRequest(string planId, string owner, string userId)
        {
            PlanId = planId;
            Owner = owner;
            UserId = userId;
        }
    }
}
