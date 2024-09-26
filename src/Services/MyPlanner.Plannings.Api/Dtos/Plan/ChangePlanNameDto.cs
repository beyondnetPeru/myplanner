namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class ChangePlanNameDto
    {
        public string PlanId { get; set; }
        public string Name { get; set; }

        public ChangePlanNameDto(string planId, string name)
        {
            PlanId = planId;
            Name = name;
        }
    }
}
