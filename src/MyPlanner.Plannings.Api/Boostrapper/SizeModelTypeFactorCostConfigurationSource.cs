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
            For<SizeModelTypeFactorCostCriteria, ISizeModelTypeFactorCostCalculator>().Create<SizeModelTypeSprintFactorCostCalculator>().When(x =>
                                                            x.Factor == "sprints" &
                                                            x.SizeModelTypeSelected == "t-shirt");
            For<SizeModelTypeFactorCostCriteria, ISizeModelTypeFactorCostCalculator>().Create<SizeModelTypeDefaultFactorCostCalculator>().When(x =>
                                                            x.Factor == "" &
                                                            x.SizeModelTypeSelected == "");
        }

    }
}
