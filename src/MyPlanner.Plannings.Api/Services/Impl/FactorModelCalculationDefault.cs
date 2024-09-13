using MyPlanner.Plannings.Api.Services.Interfaces;

namespace MyPlanner.Plannings.Api.Services.Impl
{
    public class ModelDefaultFactorDefaultCalculation : IFactorModelSizeCalculation
    {
        public double Calculate(string sizeModelTypeFactor,
                           string sizeModelTypeValueSelected,
                           double profileAvgRate)
        {
            return 0.00;
        }
    }
}
