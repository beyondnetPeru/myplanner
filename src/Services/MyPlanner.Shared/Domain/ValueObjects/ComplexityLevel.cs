using BeyondNet.Ddd;

namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class ComplexityLevel : Enumeration
    {
        public static ComplexityLevel Low = new ComplexityLevel(1, nameof(Low));
        public static ComplexityLevel Medium = new ComplexityLevel(2, nameof(Medium));
        public static ComplexityLevel High = new ComplexityLevel(3, nameof(High));
        public static ComplexityLevel VeryHigh = new ComplexityLevel(4, nameof(VeryHigh));

        public ComplexityLevel(int id, string name) : base(id, name)
        {
        }

    }
}
