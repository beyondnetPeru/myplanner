namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class ActivatePlanDto
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }

        public ActivatePlanDto(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }

    }
}
