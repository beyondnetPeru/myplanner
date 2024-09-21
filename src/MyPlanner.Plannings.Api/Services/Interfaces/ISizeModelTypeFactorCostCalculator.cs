using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Services.Interfaces
{
    public interface ISizeModelTypeFactorCostCalculator
    {
        double Calculate(FactorsEnum factor,
                         string sizeModelTypeItemValueSelected,
                         double profileAvgRate);
    }
}
