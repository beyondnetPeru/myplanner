namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class UserId : ValueObject<string>
    {
        private UserId(string value) : base(value)
        {

        }

        public static UserId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new UserId(value);
        }

        public override void AddValidators()
        {
            base.AddValidators();
        }

        public static UserId Default()
        {
            return new UserId("default");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
