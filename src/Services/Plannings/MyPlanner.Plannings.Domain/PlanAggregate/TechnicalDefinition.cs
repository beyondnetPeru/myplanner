using BeyondNet.Ddd.ValueObjects;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class TechnicalDefinition : StringValueObject
    {
        protected TechnicalDefinition(string value) : base(value)
        {
        }

        public static TechnicalDefinition Create(string value)
        {
            return new TechnicalDefinition(value);
        }

        public static TechnicalDefinition DefaultValue()
        {
            return new TechnicalDefinition("It does not have technical definition");
        }
    }
}
