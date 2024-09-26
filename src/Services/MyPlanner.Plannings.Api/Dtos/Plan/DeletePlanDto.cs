namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class DeletePlanDto
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }

        public DeletePlanDto(string planId, string userId)
        {
            PlanId = planId;
            UserId = userId;
        }
    }
}
