using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class CreateSizeModelItemDto
    {
        public string SizeModelId { get; set; }
        public string SizeModelTypeItemId { get; set; }
        public string SizeModelTypeItemCode { get; set; }
        public int FactorSelected { get; set; }
        public string ProfileName { get; set; }
        public int ProfileAvgRateSymbol { get; set; }
        public double ProfileAvgRateValue { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public bool IsStandard { get; set; }
        public string UserId { get; }

        public CreateSizeModelItemDto(string sizeModelId,
                                      string sizeModelTypeItemId,
                                      string sizeModelTypeItemCode,
                                      int factorSelected,
                                      string profileName,
                                      int profileAvgRateSymbol,
                                      double profileAvgRateValue,
                                      int quantity,
                                      double totalCost,
                                      bool isStandard,
                                      string userId)

        {
            SizeModelId = sizeModelId;
            SizeModelTypeItemId = sizeModelTypeItemId;
            SizeModelTypeItemCode = sizeModelTypeItemCode;
            FactorSelected = factorSelected;
            ProfileName = profileName;
            ProfileAvgRateSymbol = profileAvgRateSymbol;
            ProfileAvgRateValue = profileAvgRateValue;
            Quantity = quantity;
            TotalCost = totalCost;
            IsStandard = isStandard;
            UserId = userId;

        }

    }
}
