using BeyondNet.Ddd.ValueObjects;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class TechnicalDefinition : StringLongValueObject
    {
        protected TechnicalDefinition(string value, int minLenght = 10, int maxLenght = 1000) : base(value, minLenght, maxLenght)
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
