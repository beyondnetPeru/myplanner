using BeyondNet.Ddd;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;
using MyPlanner.Plannings.Domain.SizeModelTypes;

namespace MyPlanner.Plannings.Domain.SizeModelTypes.Validators
{
    public class SizeModelTypeCodeValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public SizeModelTypeCodeValidator(SizeModelTypeCode subject) : base(subject)
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
