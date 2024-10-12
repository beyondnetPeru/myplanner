namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlan
{
    public class DeactivatePlanCommand : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }
        public DeactivatePlanCommand(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
