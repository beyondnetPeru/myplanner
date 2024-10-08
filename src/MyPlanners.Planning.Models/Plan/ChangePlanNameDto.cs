namespace MyPlanner.Plannings.Models.Plan
{
    public class ChangePlanNameDto : AbstractUserDto
    {
        public string PlanId { get; set; }
        public string Name { get; set; }

        public ChangePlanNameDto(string planId, string name, string userId) : base(userId)
        {
            PlanId = planId;
            Name = name;
        }
    }
}
