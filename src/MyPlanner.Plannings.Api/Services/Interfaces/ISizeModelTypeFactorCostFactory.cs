using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Services.Interfaces
{
    public interface ISizeModelTypeFactorCostFactory
    {
        ISizeModelTypeFactorCostCalculator Create(FactorsEnum factor,
                         string sizeModelTypeCodeSelected);
    }
}
