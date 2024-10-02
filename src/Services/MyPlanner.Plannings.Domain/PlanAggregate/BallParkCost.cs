namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class BallParkCostProps : IProps
    {
        public AmountCost BallParkCost { get; private set; } = AmountCost.DefaultValue();
        public AmountCost BallparkDependenciesCost { get; private set; } = AmountCost.DefaultValue();
        public AmountCost BallParkTotalCost { get; private set; } = AmountCost.DefaultValue();

        public BallParkCostProps(int symbol, double cost, double dependenciesCost)
        {
            var symbolValue = Enumeration.FromValue<CurrencySymbolEnum>(symbol);

            BallParkCost = AmountCost.Create(symbolValue, cost);
            BallparkDependenciesCost = AmountCost.Create(symbolValue, dependenciesCost);
            BallParkTotalCost = AmountCost.Create(symbolValue, cost + dependenciesCost);
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

        public static BallParkCost Create(int symbol, double cost, double dependenciesCost)
        {
            return new BallParkCost(new BallParkCostProps(symbol, cost, dependenciesCost));
        }

        public static BallParkCost DefaultValue()
        {
            return new BallParkCost(new BallParkCostProps(CurrencySymbolEnum.USD.Id, 0.00, 0.00));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.BallParkCost.ToString() + Value.BallparkDependenciesCost.ToString() + Value.BallParkTotalCost.ToString();
        }
    }
}
