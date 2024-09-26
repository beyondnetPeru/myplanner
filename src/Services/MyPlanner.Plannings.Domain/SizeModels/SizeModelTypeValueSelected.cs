using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelTypeValueSelected : ValueObject<string>
    {
        private SizeModelTypeValueSelected(string value) : base(value)
        {
        }

        public static SizeModelTypeValueSelected Create(string value)
        {
            return new SizeModelTypeValueSelected(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
