namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class DeactivatePlanDto
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }

        public DeactivatePlanDto(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
