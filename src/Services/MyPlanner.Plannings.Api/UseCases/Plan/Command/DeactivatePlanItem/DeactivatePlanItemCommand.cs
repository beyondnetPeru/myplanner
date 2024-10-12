namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlanItem
{
    public class DeactivatePlanItemCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public string UserId { get; set; }

        public DeactivatePlanItemCommand(string planItemId, string userId)
        {
            PlanItemId = planItemId;
            UserId = userId;
        }

    }
}
