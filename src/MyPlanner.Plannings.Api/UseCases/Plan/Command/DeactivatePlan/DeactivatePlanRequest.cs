namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlan
{
    public class DeactivatePlanRequest : IRequest<bool>
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
