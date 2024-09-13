namespace MyPlanner.Plannings.Api.Services.Interfaces
{
    public interface IFactorModelSizeCalculation
    {
        double Calculate(string sizeModelTypeFactor,
                         string sizeModelTypeValueSelected,
                         double profileAvgRate);
    }
}
