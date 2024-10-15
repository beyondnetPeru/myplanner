using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Services.Impl
{
    public static class FactorCostCalculatorTotal
    {
        public static double SetTotalCost(IFactorCostCalculatorFactory sizeModelTypeFactorCostFactory, SizeModelItem sizeModelItem, SizeModel sizeModel)
        {
            var factor = sizeModelItem.GetPropsCopy().FactorSelected;
            var sizeModelTypeCode = sizeModel.GetPropsCopy().SizeModelTypeCode.GetValue();

            var costCalculator = sizeModelTypeFactorCostFactory.Create(factor, sizeModelTypeCode);

            return costCalculator.Calculate(factor,
                                    sizeModelItem.GetPropsCopy().SizeModelTypeItemCode.GetValue(),
                                    sizeModelItem.GetPropsCopy().Quantity.GetValue(),
                                    sizeModelItem.GetPropsCopy().Profile.GetValue().ProfileAvgRate.GetValue().Value);
        }
    }
}
