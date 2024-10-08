using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Services.Interfaces
{
    public interface IFactorCostCalculatorFactory
    {
        IFactorCostCalculator Create(FactorsEnum factor,
                         string sizeModelTypeCodeSelected);
    }
}
