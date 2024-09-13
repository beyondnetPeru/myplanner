using BeyondNet.Ddd.ValueObjects;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class ComponentsImpacted : StringLongValueObject
    {
        protected ComponentsImpacted(string value, int minLenght = 10, int maxLenght = 1000) : base(value, minLenght, maxLenght)
        {
        }

        public static ComponentsImpacted Create(string value)
        {
            return new ComponentsImpacted(value);
        }

        public static ComponentsImpacted DefaultValue()
        {
            return new ComponentsImpacted("It does not have components impacted");
        }
    }
}
