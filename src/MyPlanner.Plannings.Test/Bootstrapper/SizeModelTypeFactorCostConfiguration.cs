using BeyondNet.Factory.Impl;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;

namespace MyPlanner.Plannings.Domain.Test.Bootstrapper
{
    public class SizeModelTypeFactorCostConfiguration : AbstractFactoryRecordSetupSource
    {
        public SizeModelTypeFactorCostConfiguration()
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
