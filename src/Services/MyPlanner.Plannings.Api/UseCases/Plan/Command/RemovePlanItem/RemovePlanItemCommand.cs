namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.RemovePlanItem
{
    public class RemovePlanItemCommand : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string PlanItemId { get; set; }
        public string UserId { get; set; }

        public RemovePlanItemCommand(string planId, string planItemId, string userId)
        {
            PlanId = planId;
            PlanItemId = planItemId;
            UserId = userId;
        }
    }
}
