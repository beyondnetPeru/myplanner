using BeyondNet.Ddd;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;
using MyPlanner.Plannings.Shared.Helpers;

namespace MyPlanner.Plannings.Shared.Domain.ValueObjects.Validators
{
    public class EmailValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public EmailValidator(ValueObject<string> subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext context)
        {
            var value = Subject!.GetValue();

            if (string.IsNullOrWhiteSpace(value))
            {
                AddBrokenRule("Email", "Email is required");
            }

            if (!string.IsNullOrWhiteSpace(value) && !EmailHelper.IsValidEmail(value))
            {
                AddBrokenRule("Email", "Email is invalid");
            }
        }
    }
}
