using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class BallParkTotalCost : ValueObject<double>
    {
        public BallParkTotalCost(double value) : base(value)
        {
        }

        public static BallParkTotalCost Create(double value)
        {
            return new BallParkTotalCost(value);
        }

        public static BallParkTotalCost DefaultValue()
        {
            return new BallParkTotalCost(0.00);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static BallParkTotalCost operator +(BallParkTotalCost a, BallParkTotalCost b)
        {
            return new BallParkTotalCost(a.Value + b.Value);
        }
    }
}
