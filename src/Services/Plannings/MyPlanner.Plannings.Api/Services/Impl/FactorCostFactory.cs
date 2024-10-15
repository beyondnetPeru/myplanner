using MyPlanner.Plannings.Api.Boostrapper;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Services.Impl
{

    public class FactorCostFactory : IFactorCostCalculatorFactory
    {
        private readonly IObjectFactory objectFactory;

        public FactorCostFactory(IObjectFactory objectFactory)
        {
            this.objectFactory = objectFactory;
        }

        public IFactorCostCalculator Create(FactorsEnum factor, string sizeModelTypeCodeSelected)
        {
            try
            {
                var criteria = new FactorCostCriteria(factor.Name.ToLower().Trim(), sizeModelTypeCodeSelected.ToLower().Trim());

                var factorCostCalculator = objectFactory.Create<FactorCostCriteria, IFactorCostCalculator>(criteria).FirstOrDefault();

                if (factorCostCalculator == null)
                {
                    return new FactorCostCalculatorDefault();
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
