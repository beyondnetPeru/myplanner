using Azure.Core;

namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class CreateSizeModelItemDto
    {
        public string SizeModelId { get; set; }
        public string ProfileName { get; set; }
        public double ProfileAvgRateAmount { get; set; }
        public string SizeModelFactorTypeCode { get; set; }
        public string ProfileValueSelected { get; set; }
        public int ProfileCountValue { get; set; }
        public double TotalCost { get; set; }
        public string UserId { get; }

        public CreateSizeModelItemDto(string sizeModelId,
                                      string profileName,
                                      double profileAvgRateAmount,
                                      string sizeModelFactorTypeCode,
                                      string profileValueSelected,
                                      int profileCountValue,
                                      double totalCost,
                                      string userId)
        {
            SizeModelId = sizeModelId;
            ProfileName = profileName;
            ProfileAvgRateAmount = profileAvgRateAmount;
            SizeModelFactorTypeCode = sizeModelFactorTypeCode;
            ProfileValueSelected = profileValueSelected;
            ProfileCountValue = profileCountValue;
            TotalCost = totalCost;
            UserId = userId;
        }
    }
}
