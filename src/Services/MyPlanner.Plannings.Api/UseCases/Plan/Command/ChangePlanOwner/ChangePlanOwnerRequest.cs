using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeOwner
{
    public class ChangePlanOwnerRequest : IRequest<bool>
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
