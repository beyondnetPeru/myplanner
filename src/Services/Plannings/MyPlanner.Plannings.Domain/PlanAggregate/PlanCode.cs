namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class PlanCode : ValueObject<string>
    {
        private PlanCode(string value) : base(value)
        {
        }
        public static PlanCode Create(string value)
        {
            return new PlanCode(value);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
