namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlanItem
{
    public class ActivatePlanItemCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public string UserId { get; set; }

        public ActivatePlanItemCommand(string planItemId, string userId)
        {
            PlanItemId = planItemId;
            UserId = userId;
        }
    }
}
