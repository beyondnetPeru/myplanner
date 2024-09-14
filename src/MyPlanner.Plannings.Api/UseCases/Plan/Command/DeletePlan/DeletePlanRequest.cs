namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeletePlan
{
    public class DeletePlanRequest : IRequest<bool>
    {
        public string PlanId { get; }
        public string UserId { get; }

        public DeletePlanRequest(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
