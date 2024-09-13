using BeyondNet.Ddd.ValueObjects;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class TechnicalDependencies : StringLongValueObject
    {
        protected TechnicalDependencies(string value, int minLenght = 10, int maxLenght = 1000) : base(value, minLenght, maxLenght)
        {
        }

        public static TechnicalDependencies Create(string value)
        {
            return new TechnicalDependencies(value);
        }

        public static TechnicalDependencies DefaultValue()
        {
            return new TechnicalDependencies("It does not have technical dependencies");
        }
    }
}
