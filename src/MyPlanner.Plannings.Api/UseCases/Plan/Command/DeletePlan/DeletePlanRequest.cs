namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeletePlan
{
    public class DeletePlanRequest : IRequest<bool>
    {
        public string PlanId { get; }

        public DeletePlanRequest(string planId)
        {
            PlanId = planId;
        }
    }
}
