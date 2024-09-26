using Jal.Factory;
using MyPlanner.Plannings.Api.Models;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;

namespace MyPlanner.Plannings.Api.Boostrapper
{
    public class SizeModelTypeFactorCostConfigurationSource : AbstractObjectFactoryConfigurationSource
    {
        public SizeModelTypeFactorCostConfigurationSource()
        {
            For<SizeModelTypeFactorCostCriteria, ISizeModelTypeFactorCostCalculator>().Create<SizeModelTypeTShirtAndSprintFactorCostCalculator>().When(x =>
                                                            x.Factor.ToLower().Trim() == "sprints" &
                                                            x.SizeModelTypeCodeSelected == "smt001");
            For<SizeModelTypeFactorCostCriteria, ISizeModelTypeFactorCostCalculator>().Create<SizeModelTypeDefaultFactorDefaultCostCalculator>().When(x =>
                                                            x.Factor == "" &
                                                            x.SizeModelTypeCodeSelected == "");
        }

    }
}
