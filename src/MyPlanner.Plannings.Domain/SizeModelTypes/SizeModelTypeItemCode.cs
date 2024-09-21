using BeyondNet.Ddd;



namespace MyPlanner.Plannings.Domain.SizeModelTypes
{
    public class SizeModelTypeItemCode : ValueObject<string>
    {
        public SizeModelTypeItemCode(string value) : base(value)
        {
        }

        public override void AddValidators()
        {
            base.AddValidators();
        }

        public static SizeModelTypeItemCode Create(string value)
        {
            return new SizeModelTypeItemCode(value);
        }

        public static SizeModelTypeItemCode DefaultValue()
        {
            return new SizeModelTypeItemCode("MTS999");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
