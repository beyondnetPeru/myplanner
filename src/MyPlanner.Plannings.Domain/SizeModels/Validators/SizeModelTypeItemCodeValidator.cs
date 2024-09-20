using BeyondNet.Ddd;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace MyPlanner.Plannings.Domain.SizeModels.Validators
{
    public class SizeModelTypeItemCodeValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public SizeModelTypeItemCodeValidator(SizeModelTypeCode subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext context)
        {
            var value = Subject!.GetValue();

            if (string.IsNullOrEmpty(value))
            {
                AddBrokenRule("Value", "Value cannot be null");
                return;
            }

            if (value.Length < 4)
            {
                AddBrokenRule("Value", "Value cannot be less than 3 characters");
                return;
            }

            if (value.Length > 6)
            {
                AddBrokenRule("Value", "Value cannot be more than 5 characters");
                return;
            }

            if (!value.StartsWith("SMT"))
            {
                AddBrokenRule("Value", "Value must start with SMT");
                return;
            }
        }
    }
}
