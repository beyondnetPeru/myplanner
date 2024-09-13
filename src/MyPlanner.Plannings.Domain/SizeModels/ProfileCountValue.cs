using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class ProfileCountValue : ValueObject<double>
    {
        private ProfileCountValue(double value) : base(value)
        {
        }

        public static ProfileCountValue Create(double value)
        {
            return new ProfileCountValue(value);
        }

        public static ProfileCountValue DefaultValue()
        {
            return new ProfileCountValue(1);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
