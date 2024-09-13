using BeyondNet.Ddd;
using MyPlanner.Plannings.Domain.SizeModels.Validators;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelTypeQuantity : ValueObject<int>
    {
        private SizeModelTypeQuantity(int value) : base(value)
        {

        }
        public static SizeModelTypeQuantity Create(int value)
        {
            return new SizeModelTypeQuantity(value);
        }

        public override void AddValidators()
        {
            base.AddValidators();

            AddValidator(new SizeModelTypeQuantityValidator(this));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
