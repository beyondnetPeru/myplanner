using BeyondNet.Factory.Impl;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;

namespace MyPlanner.Plannings.Domain.Test.Bootstrapper
{
    public class SizeModelTypeFactorCostConfiguration : AbstractFactoryRecordSetupSource
    {
        public SizeModelTypeFactorCostConfiguration()
        {
            For<Criteria, IFactorCostCalculator>().Create<FactorCostCalculatorTShirtAndSprint>().When(x =>
                                                            x.Factor == "sprints" &
                                                            x.SizeModelTypeSelected == "t-shirt");
            For<Criteria, IFactorCostCalculator>().Create<FactorCostCalculatorDefault>().When(x =>
                                                            x.Factor == "" &
                                                            x.SizeModelTypeSelected == "");
        }

    }
}
