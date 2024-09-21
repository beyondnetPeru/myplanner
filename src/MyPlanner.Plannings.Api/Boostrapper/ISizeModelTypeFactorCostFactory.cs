using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Boostrapper
{
    public interface ISizeModelTypeFactorCostFactory
    {
        ISizeModelTypeFactorCostCalculator Create(FactorsEnum factor,
                         string sizeModelTypeItemValueSelected);
    }
}
