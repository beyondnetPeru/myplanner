namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class CreateSizeModelItemDto
    {
        public string SizeModelId { get; set; }
        public string ProfileName { get; set; }
        public double ProfileAvgRateAmount { get; set; }
        public string SizeModelFactorTypeId { get; set; }
        public string ProfileValueSelected { get; set; }
        public int ProfileCountValue { get; set; }
        public double TotalCost { get; set; }
        public string UserId { get; }

        public CreateSizeModelItemDto(string sizeModelId,
                                      string profileName,
                                      double profileAvgRateAmount,
                                      string sizeModelFactorTypeId,
                                      string profileValueSelected,
                                      int profileCountValue,
                                      double totalCost,
                                      string userId)
        {
            SizeModelId = sizeModelId;
            ProfileName = profileName;
            ProfileAvgRateAmount = profileAvgRateAmount;
            SizeModelFactorTypeId = sizeModelFactorTypeId;
            ProfileValueSelected = profileValueSelected;
            ProfileCountValue = profileCountValue;
            TotalCost = totalCost;
            UserId = userId;

        }
    }
}
