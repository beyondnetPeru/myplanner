namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName
{
    public class ChangePlanNameCommand : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }

        public ChangePlanNameCommand(string planId, string name, string userId)
        {
            PlanId = planId;
            Name = name;
            UserId = userId;
        }
    }
}
