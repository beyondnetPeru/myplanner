using Jal.Factory;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Boostrapper
{

    public class SizeModelTypeFactorCostFactory : ISizeModelTypeFactorCostFactory
    {
        private readonly IObjectFactory objectFactory;

        public SizeModelTypeFactorCostFactory(IObjectFactory objectFactory)
        {
            this.objectFactory = objectFactory;
        }

        public ISizeModelTypeFactorCostCalculator Create(FactorsEnum factor, string sizeModelTypeItemValueSelected)
        {
            try
            {
                var criteria = new Criteria(factor.Name, sizeModelTypeItemValueSelected);

                var factorCostCalculator = objectFactory.Create<Criteria, ISizeModelTypeFactorCostCalculator>(criteria).FirstOrDefault();

                if (factorCostCalculator == null)
                {
                    return new SizeModelTypeDefaultFactorCostCalculator();
                }

                return factorCostCalculator;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
