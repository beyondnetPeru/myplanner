namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeAssumptions
{
    public class ChangeAssumptionsCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public string Assumptions { get; set; }
        public string UserId { get; set; }

        public ChangeAssumptionsCommand(string planItemId, string assumptions, string userId)
        {
            PlanItemId = planItemId;
            Assumptions = assumptions;
            UserId = userId;
        }
    }
}
