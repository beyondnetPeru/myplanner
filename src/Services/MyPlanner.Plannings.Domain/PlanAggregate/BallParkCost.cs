namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class BallParkCostProps : IProps
    {
        public AmountCost BallParkCost { get; private set; } = AmountCost.DefaultValue();
        public AmountCost BallparkDependenciesCost { get; private set; } = AmountCost.DefaultValue();
        public AmountCost BallParkTotalCost { get; private set; } = AmountCost.DefaultValue();

        public BallParkCostProps(CurrencySymbolEnum symbol, double cost, double dependenciesCost)
        {
            BallParkCost = AmountCost.Create(symbol, cost);
            BallparkDependenciesCost = AmountCost.Create(symbol, dependenciesCost);
            BallParkTotalCost = AmountCost.Create(symbol, cost + dependenciesCost);
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

        public static BallParkCost Create(CurrencySymbolEnum symbol, double cost, double dependenciesCost)
        {
            return new BallParkCost(new BallParkCostProps(symbol, cost, dependenciesCost));
        }

        public static BallParkCost DefaultValue()
        {
            return new BallParkCost(new BallParkCostProps(CurrencySymbolEnum.USD, 0.00, 0.00));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.BallParkCost.ToString() + Value.BallparkDependenciesCost.ToString() + Value.BallParkTotalCost.ToString();
        }
    }
}
