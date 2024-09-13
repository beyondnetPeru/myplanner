using BeyondNet.Ddd;



namespace MyPlanner.Plannings.Domain.SizeModels
{
     public class SizeModelTypeFactorCode : ValueObject<string>
    {
        public SizeModelTypeFactorCode(string value) : base(value)
        {
        }

        public override void AddValidators()
        {
            base.AddValidators();
        }

        public static SizeModelTypeFactorCode Create(string value)
        {
            return new SizeModelTypeFactorCode(value);
        }

        public static SizeModelTypeFactorCode DefaultValue()
        {
            return new SizeModelTypeFactorCode("MTS999");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
