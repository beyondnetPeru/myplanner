namespace MyPlanner.Plannings.Models.Plan
{
    public class DeletePlanDto : AbstractUserDto
    {
        public string PlanId { get; set; }

        public DeletePlanDto(string planId, string userId) : base(userId)
        {
            PlanId = planId;
        }
    }
}
