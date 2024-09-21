
using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelItemIsStandard : ValueObject<bool>
    {
        public SizeModelItemIsStandard(bool value) : base(value)
        {
        }

        public static SizeModelItemIsStandard Create(bool value)
        {
            return new SizeModelItemIsStandard(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
