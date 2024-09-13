using BeyondNet.Ddd;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace MyPlanner.Plannings.Domain.SizeModels.Validators
{
    public class SizeModelTypeQuantityValidator : AbstractRuleValidator<ValueObject<int>>
    {
        public SizeModelTypeQuantityValidator(SizeModelTypeQuantity subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext context)
        {
            if (Subject!.GetValue() < 1 || Subject.GetValue() > 30)
            {
                AddBrokenRule("SizeModelTypeQuantity", "SizeModelTypeQuantity must be between 1 and 30");
                return;
            }
        }
    }
}
