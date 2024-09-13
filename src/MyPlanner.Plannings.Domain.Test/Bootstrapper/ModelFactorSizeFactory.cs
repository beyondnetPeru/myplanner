using BeyondNet.Factory.Impl;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;

namespace MyPlanner.Plannings.Domain.Test.Bootstrapper
{
    public class ModelFactorSizeFactory : AbstractFactoryRecordSetupSource
    {
        public ModelFactorSizeFactory()
        {
            For<Criteria, IFactorModelSizeCalculation>().Create<ModelTShirtFactorSprintCalculation>().When(x =>
                                                            x.SizeModelType.Name == "tshirt" &
                                                            x.SizeModelTypeFactorSelected == "sprints");
            For<Criteria, IFactorModelSizeCalculation>().Create<ModelDefaultFactorDefaultCalculation>().When(x =>
                                                            x.SizeModelType.Name == "tshirt" &
                                                            x.SizeModelTypeFactorSelected == "mandays");
        }

    }
}
