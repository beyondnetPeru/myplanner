namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class ComplexityLevelEnum : Enumeration
    {
        public static ComplexityLevelEnum Low = new ComplexityLevelEnum(1, "Low");
        public static ComplexityLevelEnum Medium = new ComplexityLevelEnum(2, "Medium");
        public static ComplexityLevelEnum High = new ComplexityLevelEnum(3, "High");
        public static ComplexityLevelEnum VeryHigh = new ComplexityLevelEnum(4, "Very High");

        public ComplexityLevelEnum(int id, string name) : base(id, name)
        {
        }

    }
}
