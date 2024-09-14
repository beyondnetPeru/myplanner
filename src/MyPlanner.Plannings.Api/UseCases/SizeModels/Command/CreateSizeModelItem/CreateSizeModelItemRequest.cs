namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModelItem
{
    public class CreateSizeModelItemRequest : IRequest<bool>
    {
        public string SizeModelId { get; set; }
        public string ProfileName { get; set; }
        public double ProfileAvgRateAmount { get; set; }
        public string SizeModelTypeFactorId { get; set; }
        public string ProfileValueSelected { get; set; }
        public int ProfileQuantity { get; set; }
        public double TotalCost { get; set; }
        public string UserId { get; }

        public CreateSizeModelItemRequest(string sizeModelId,
                                          string profileName,
                                          double profileAvgRateAmount,
                                          string sizeModelTypeFactorId,
                                          string profileValueSelected,
                                          int profileCountValue,
                                          double totalCost,
                                          string userId)
        {
            SizeModelId = sizeModelId;
            ProfileName = profileName;
            ProfileAvgRateAmount = profileAvgRateAmount;
            SizeModelTypeFactorId = sizeModelTypeFactorId;
            ProfileValueSelected = profileValueSelected;
            ProfileQuantity = profileCountValue;
            TotalCost = totalCost;
            UserId = userId;
        }
    }
}


