namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class DraftPlanDto
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }

        public DraftPlanDto(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }

    }
}
