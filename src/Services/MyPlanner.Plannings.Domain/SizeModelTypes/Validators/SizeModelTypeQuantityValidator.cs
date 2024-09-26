using BeyondNet.Ddd;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Domain.SizeModelTypes.Validators
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
