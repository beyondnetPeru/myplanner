namespace MyPlanner.Planning.Models.Plan
{
    public class ChangePlanItemTypeDto : AbstractUserDto
    {
        public ChangePlanItemTypeDto(int planItemType, string userId) : base(userId)
        {
            PlanItemType = planItemType;
        }

        public int PlanItemType { get; }
    }
}
