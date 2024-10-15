using BeyondNet.Ddd;
using MyPlanner.Plannings.Domain.SizeModelTypes.Validators;


namespace MyPlanner.Plannings.Domain.SizeModelTypes
{
    public class SizeModelTypeCode : ValueObject<string>
    {
        public SizeModelTypeCode(string value) : base(value)
        {
        }

        public override void AddValidators()
        {
            base.AddValidators();

            AddValidator(new SizeModelTypeCodeValidator(this));
        }

        public static SizeModelTypeCode Create(string value)
        {
            return new SizeModelTypeCode(value);
        }

        public static SizeModelTypeCode DefaultValue()
        {
            return new SizeModelTypeCode("MTS999");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
