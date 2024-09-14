namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlan
{
    public class ActivatePlanRequest : IRequest<bool>
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
