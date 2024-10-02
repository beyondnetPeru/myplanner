namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class CategoryName : ValueObject<string>
    {
        private CategoryName(string value) : base(value)
        {
        }

        public static CategoryName Create(string value)
        {
            return new CategoryName(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
