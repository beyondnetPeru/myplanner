namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class ClosePlanDto
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }
        public ClosePlanDto(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
