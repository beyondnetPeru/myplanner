namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class BallParkCostProps : IProps
    {
        public double Cost { get; private set; } = 0.00;
        public double DependenciesCost { get; private set; } = 0.00;
        public double TotalCost { get; private set; } = 0.00;

        public BallParkCostProps(double cost, double dependenciesCost)
        {
            Cost = cost;
            DependenciesCost = dependenciesCost;
            TotalCost = cost + dependenciesCost;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    public class BallParkCost : ValueObject<BallParkCostProps>
    {
        public BallParkCost(BallParkCostProps value) : base(value)
        {
        }

        public static BallParkCost Create(double cost, double dependenciesCost)
        {
            return new BallParkCost(new BallParkCostProps(cost, dependenciesCost));
        }

        public static BallParkCost DefaultValue()
        {
            return new BallParkCost(new BallParkCostProps(0.00, 0.00));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.Cost.ToString() + Value.DependenciesCost.ToString() + Value.TotalCost.ToString();
        }
    }
}
