using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelTotalCost : ValueObject<double>
    {
        private SizeModelTotalCost(double value) : base(value)
        {
        }

        public static SizeModelTotalCost Create(double value)
        {
            return new SizeModelTotalCost(value);
        }

        public static SizeModelTotalCost DefaultValue()
        {
            return new SizeModelTotalCost(1);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
