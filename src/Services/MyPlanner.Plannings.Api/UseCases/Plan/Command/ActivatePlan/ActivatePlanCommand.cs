namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlan
{
    public class ActivatePlanCommand : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }

        public ActivatePlanCommand(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
