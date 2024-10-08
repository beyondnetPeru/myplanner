namespace MyPlanner.Plannings.Models.Plan
{
    public class ActivatePlanDto : AbstractUserDto
    {
        public string PlanId { get; set; }
        public ActivatePlanDto(string planId, string userId): base(userId)
        {
            PlanId = planId;
        }
    }
}
