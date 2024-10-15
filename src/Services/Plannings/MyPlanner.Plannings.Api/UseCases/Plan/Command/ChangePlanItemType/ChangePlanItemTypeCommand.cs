namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangePlanItemType
{
    public class ChangePlanItemTypeCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public int PlanItemType { get; set; }
        public string UserId { get; set; }
        public ChangePlanItemTypeCommand(string planItemId, int planItemType, string userId)
        {
            PlanItemId = planItemId;
            PlanItemType = planItemType;
            UserId = userId;
        }
    }
}
