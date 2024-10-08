using BeyondNet.Ddd.ValueObjects;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class ComponentsImpacted : StringValueObject
    {
        protected ComponentsImpacted(string value) : base(value)
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
