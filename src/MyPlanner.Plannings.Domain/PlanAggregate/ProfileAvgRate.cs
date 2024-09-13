using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class ProfileAvgRate : ValueObject<double>
    {
        private ProfileAvgRate(double value) : base(value)
        {
        }

        public static ProfileAvgRate Create(double value)
        {
            return new ProfileAvgRate(value);
        }

        public static ProfileAvgRate Defaultvalue()
        {
            return new ProfileAvgRate(0);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
