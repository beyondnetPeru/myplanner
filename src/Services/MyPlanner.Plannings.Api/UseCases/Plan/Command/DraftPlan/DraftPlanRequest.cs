namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DraftPlan
{
    public class DraftPlanRequest : IRequest<bool>
    {
        public string PlanId { get; set; }
        public string UserId { get; }

        public DraftPlanRequest(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
