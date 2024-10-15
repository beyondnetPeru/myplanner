using BeyondNet.Ddd.ValueObjects;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class TechnicalDependencies : StringValueObject
    {
        protected TechnicalDependencies(string value) : base(value)
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
