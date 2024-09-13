using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class KeyAssumptions : ValueObject<string>
    {
        protected KeyAssumptions(string value) : base(value)
        {
        }

        public static KeyAssumptions Create(string value)
        {
            return new KeyAssumptions(value);
        }

        public static KeyAssumptions DefaultValue()
        {
            return new KeyAssumptions("It does not have key assumptions");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
