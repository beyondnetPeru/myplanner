using MyPlanner.Plannings.Api.Boostrapper;
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

        public ISizeModelTypeFactorCostCalculator Create(FactorsEnum factor, string sizeModelTypeCodeSelected)
        {
            try
            {
                var criteria = new FactorCostCriteria(factor.Name.ToLower().Trim(), sizeModelTypeCodeSelected.ToLower().Trim());

                var factorCostCalculator = objectFactory.Create<FactorCostCriteria, ISizeModelTypeFactorCostCalculator>(criteria).FirstOrDefault();

                if (factorCostCalculator == null)
                {
                    return new SizeModelTypeDefaultFactorDefaultCostCalculator();
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
