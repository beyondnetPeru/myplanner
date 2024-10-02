
namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class AmountCostProps : IProps
    {
        public CurrencySymbolEnum Symbol { get; private set; }
        public double Amount { get; private set; }

        public AmountCostProps(CurrencySymbolEnum symbol, double amount)
        {
            Symbol = symbol;
            Amount = amount;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class AmountCost : ValueObject<AmountCostProps>
    {
        private AmountCost(AmountCostProps value) : base(value)
        {
        }
        public static AmountCost Create(CurrencySymbolEnum symbol, double amount)
        {
            return new AmountCost(new AmountCostProps(symbol, amount));
        }

        public static AmountCost DefaultValue()
        {
            return new AmountCost(new AmountCostProps(CurrencySymbolEnum.USD, 0.00));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.Symbol.ToString() + Value.Amount.ToString();
        }
    }
}
