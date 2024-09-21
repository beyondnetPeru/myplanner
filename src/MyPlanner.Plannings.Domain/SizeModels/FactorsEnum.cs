using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class FactorsEnum : Enumeration
    {
        public static readonly FactorsEnum ManDays = new FactorsEnum(1, "ManDays");
        public static readonly FactorsEnum Sprints = new FactorsEnum(2, "Sprints");
        public static readonly FactorsEnum Percentage = new FactorsEnum(3, "Percentage");

        public FactorsEnum(int id, string name)
            : base(id, name)
        {
        }
    }
}
