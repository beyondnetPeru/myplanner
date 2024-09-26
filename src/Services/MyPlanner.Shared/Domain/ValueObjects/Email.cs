using BeyondNet.Ddd;
using MyPlanner.Shared.Domain.ValueObjects.Validators;

namespace MyPlanner.Projects.Domain.ValueObjects
{
    public class Email : ValueObject<string>
    {
        public Email(string value) : base(value)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override void AddValidators()
        {
            base.AddValidators();

            AddValidator(new EmailValidator(this));

        }

    }
}
