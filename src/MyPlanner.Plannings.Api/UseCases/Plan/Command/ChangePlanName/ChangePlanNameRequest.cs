namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName
{
    public class ChangePlanNameRequest : IRequest<bool>
    {
        public string PlanId { get; set; }
        public string Name { get; set; }

        public ChangePlanNameRequest(string planId, string name)
        {
            PlanId = planId;
            Name = name;
        }
    }
}
