namespace MyPlanner.Plannings.Models.Plan
{
    public class DraftPlanDto : AbstractUserDto
    {
        public string PlanId { get; set; }

        public DraftPlanDto(string planId, string userId) : base(userId)
        {
            PlanId = planId;
        }
    }
}
