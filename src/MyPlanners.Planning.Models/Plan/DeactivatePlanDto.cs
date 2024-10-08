namespace MyPlanner.Plannings.Models.Plan
{
    public class DeactivatePlanDto : AbstractUserDto
    {
        public string PlanId { get; set; }

        public DeactivatePlanDto(string planId, string userId) : base(userId)
        {
            PlanId = planId;
        }
    }
}
