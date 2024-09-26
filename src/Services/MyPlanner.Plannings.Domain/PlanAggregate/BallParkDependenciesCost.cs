using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class BallParkDependenciesCost : ValueObject<double>
    {
        public BallParkDependenciesCost(double value) : base(value)
        {
        }

        public static BallParkDependenciesCost Create(double value)
        {
            return new BallParkDependenciesCost(value);
        }

        public static BallParkDependenciesCost DefaultValue()
        {
            return new BallParkDependenciesCost(0.00);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static BallParkDependenciesCost operator +(BallParkDependenciesCost a, BallParkDependenciesCost b)
        {
            return new BallParkDependenciesCost(a.Value + b.Value);
        }
    }
}
