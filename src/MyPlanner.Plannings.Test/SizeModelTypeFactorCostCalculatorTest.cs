using LightInject;
using BeyondNet.Factory.Installer;
using MyPlanner.Plannings.Domain.Test.Bootstrapper;
using Shouldly;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;

namespace MyPlanner.Plannings.Domain.Test
{
    [TestClass]
    public class SizeModelTypeFactorCostCalculatorTest
    {
        IServiceContainer container = new ServiceContainer();

        [TestInitialize]
        public void Setup()
        {
            container.AddFactory(c =>
            {
                c.AddSource<SizeModelTypeFactorCostConfiguration>();
                c.AddSingleton<ISizeModelTypeFactorCostCalculator, SizeModelTypeDefaultFactorCostCalculator>();
                c.AddSingleton<ISizeModelTypeFactorCostCalculator, SizeModelTypeSprintFactorCostCalculator>();
            });
        }

        [TestMethod]
        public void ShouldCreateAndIntanceFromFactoryBuilderSuccessfully()
        {
            var factory = container.GetFactory();

            var criteria = new Criteria("sprints", "t-shirt");

            var factoryModelCalculation = factory.Create<Criteria, ISizeModelTypeFactorCostCalculator>(criteria)[0];

            var typeOf = factoryModelCalculation.GetType().Name;

            typeOf.ShouldBe("SizeModelTypeSprintFactorCostCalculator");
        }


        [TestCleanup]
        public void Cleanup()
        {
            container.Dispose();
        }
    }
}
