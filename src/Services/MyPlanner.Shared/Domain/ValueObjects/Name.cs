using BeyondNet.Ddd;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;
using BeyondNet.Ddd.ValueObjects;

namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class Name : StringRequiredValueObject
    {
        protected Name(string value) : base(value)
        {

        }

        public override void AddValidators()
        {
            base.AddValidators();

            AddValidator(new NameValidator(this));
        }

        public static Name Create(string value)
        {
            return new Name(value);
        }

    }

    public class NameValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public NameValidator(ValueObject<string> subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext context)
        {
            if (Subject!.GetValue().ToString()!.Length < 3 && Subject!.GetValue().ToString()!.Length > 300)
            {
                AddBrokenRule("Value", "Value must be between 3 and 300 characters");
            }
        }
    }
}
