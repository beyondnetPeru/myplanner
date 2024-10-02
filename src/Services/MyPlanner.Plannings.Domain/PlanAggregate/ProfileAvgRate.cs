namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class ProfileAvgRateProps
    {
        public CurrencySymbolEnum Symbol { get; set; }
        public double Value { get; set; } = 0;

        public ProfileAvgRateProps(CurrencySymbolEnum symbol, double value)
        {
            Symbol = symbol;
            Value = value;
        }
    }
    public class ProfileAvgRate : ValueObject<ProfileAvgRateProps>
    {
        public ProfileAvgRate(ProfileAvgRateProps value) : base(value)
        {
        }

        public static ProfileAvgRate Create(CurrencySymbolEnum symbol, double value)
        {
            return new ProfileAvgRate(new ProfileAvgRateProps(symbol, value));
        }

        public static ProfileAvgRate Defaultvalue()
        {
            return new ProfileAvgRate(new ProfileAvgRateProps(CurrencySymbolEnum.USD, 0.00));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
