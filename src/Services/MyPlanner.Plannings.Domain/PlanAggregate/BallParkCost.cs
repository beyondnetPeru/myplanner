using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class BallParkCost : ValueObject<double>
    {
        public BallParkCost(double value) : base(value)
        {
        }

        public static BallParkCost Create(double value)
        {
            return new BallParkCost(value);
        }

        public static BallParkCost DefaultValue()
        {
            return new BallParkCost(0.00);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static BallParkCost operator +(BallParkCost a, BallParkCost b)
        {
            return new BallParkCost(a.Value + b.Value);
        }


        public static BallParkCost operator -(BallParkCost a, BallParkCost b)
        {
            return new BallParkCost(a.Value - b.Value);
        }
    }
}
