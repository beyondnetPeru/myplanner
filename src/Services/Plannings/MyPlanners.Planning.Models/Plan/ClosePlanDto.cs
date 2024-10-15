namespace MyPlanner.Plannings.Models.Plan
{
    public class ClosePlanDto : AbstractUserDto
    {
        public string PlanId { get; set; }
        public ClosePlanDto(string planId, string userId) : base(userId)
        {
            PlanId = planId;
        }
    }
}
