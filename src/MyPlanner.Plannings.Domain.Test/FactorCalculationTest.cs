using LightInject;
using BeyondNet.Factory.Installer;
using MyPlanner.Plannings.Domain.Test.Bootstrapper;
using MyPlanner.Plannings.Domain.SizeModels;
using Shouldly;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Api.Services.Impl;

namespace MyPlanner.Plannings.Domain.Test
{
    [TestClass]
    public class FactorCalculationTest
    {
        IServiceContainer container = new ServiceContainer();

        [TestInitialize]
        public void Setup()
        {
            container.AddFactory(c =>
            {
                c.AddSource<ModelFactorSizeFactory>();
                c.AddSingleton<IFactorModelSizeCalculation, ModelDefaultFactorDefaultCalculation>();
                c.AddSingleton<IFactorModelSizeCalculation, ModelTShirtFactorSprintCalculation>();
            });
        }

        [TestMethod]
        public void ShouldCreateAndIntanceFromFactoryBuilderSuccessfully()
        {
            var factory = container.GetFactory();

            var criteria = new Criteria()
            {
                SizeModelType = SizeModelTypeEnum.TShirt,
                SizeModelTypeFactorSelected = "sprint"
            };

            var factoryModelCalculation = factory.Create<Criteria, IFactorModelSizeCalculation>(criteria)[0];

            var typeOf = factoryModelCalculation.GetType().Name;

            typeOf.ShouldBe("FactorSprintModelTShirtCalculation");
        }


        [TestCleanup]
        public void Cleanup()
        {
            container.Dispose();
        }
    }
}
