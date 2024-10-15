namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class PlanItemTypeEnum : Enumeration
    {
        public static PlanItemTypeEnum Plan = new PlanItemTypeEnum(1, "Plan");
        public static PlanItemTypeEnum Idea = new PlanItemTypeEnum(2, "Idea");
        public PlanItemTypeEnum(int id, string name) : base(id, name)
        {
        }
    }
}
