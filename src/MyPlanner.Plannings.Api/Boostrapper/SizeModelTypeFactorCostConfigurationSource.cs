using Jal.Factory;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;

namespace MyPlanner.Plannings.Api.Boostrapper
{
    public class SizeModelTypeFactorCostConfigurationSource : AbstractObjectFactoryConfigurationSource
    {
        public SizeModelTypeFactorCostConfigurationSource()
        {
            For<Criteria, ISizeModelTypeFactorCostCalculator>().Create<SizeModelTypeSprintFactorCostCalculator>().When(x =>
                                                            x.Factor == "sprints" &
                                                            x.SizeModelTypeSelected == "t-shirt");
            For<Criteria, ISizeModelTypeFactorCostCalculator>().Create<SizeModelTypeDefaultFactorCostCalculator>().When(x =>
                                                            x.Factor == "" &
                                                            x.SizeModelTypeSelected == "");
        }

    }
}
