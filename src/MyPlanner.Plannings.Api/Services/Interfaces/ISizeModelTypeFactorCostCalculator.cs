using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Services.Interfaces
{
    public interface ISizeModelTypeFactorCostCalculator
    {
        double Calculate(FactorsEnum factor,// sprints, mandays
                         string sizeModelTypeItemCode,// xs, s, m, sm, l,xl
                         int quantity, // 2
                         double profileAvgRate); //5000
    }
}
