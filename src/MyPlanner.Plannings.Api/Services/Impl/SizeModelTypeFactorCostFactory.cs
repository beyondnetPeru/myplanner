using Jal.Factory;
using MyPlanner.Plannings.Api.Models;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Services.Impl
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
                var criteria = new SizeModelTypeFactorCostCriteria(factor.Name, sizeModelTypeItemValueSelected);

                var factorCostCalculator = objectFactory.Create<SizeModelTypeFactorCostCriteria, ISizeModelTypeFactorCostCalculator>(criteria).FirstOrDefault();

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
