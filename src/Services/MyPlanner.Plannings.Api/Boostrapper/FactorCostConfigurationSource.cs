using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Boostrapper
{
    /// <summary>
    /// Represents a configuration source for SizeModelTypeFactorCost.
    /// </summary>
    public class FactorCostConfigurationSource : AbstractObjectFactoryConfigurationSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FactorCostConfigurationSource"/> class.
        /// </summary>
        public FactorCostConfigurationSource()
        {
            For<FactorCostCriteria, ISizeModelTypeFactorCostCalculator>().Create<SizeModelTypeTShirtAndSprintFactorCostCalculator>().When(x =>
                                                            x.Factor.ToLower().Trim() == FactorsEnum.Sprints.Name.Trim().ToLowerInvariant() &
                                                            x.SizeModelTypeCodeSelected == "smt001");
            For<FactorCostCriteria, ISizeModelTypeFactorCostCalculator>().Create<SizeModelTypeDefaultFactorDefaultCostCalculator>().When(x =>
                                                            x.Factor == "" &
                                                            x.SizeModelTypeCodeSelected == "");
        }
    }
}
