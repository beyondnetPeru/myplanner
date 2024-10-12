namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeComponentsImpacted
{
    public class ChangeComponentsImpactedCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public string ComponentsImpacted { get; set; }
        public string UserId { get; set; }

        public ChangeComponentsImpactedCommand(string planItemId, string componentsImpacted, string userId)
        {
            PlanItemId = planItemId;
            ComponentsImpacted = componentsImpacted;
            UserId = userId;
        }

    }
}
