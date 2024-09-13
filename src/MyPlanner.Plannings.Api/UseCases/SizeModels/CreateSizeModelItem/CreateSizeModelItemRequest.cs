using MediatR;


namespace MyPlanner.Plannings.Api.UseCases.SizeModels.CreateSizeModelItem
{
    public class CreateSizeModelItemRequest : IRequest<bool>
    {
        public string SizeModelId { get; set; }
        public string ProfileName { get; set; }
        public double ProfileAvgRateAmount { get; set; }
        public string SizeModelTypeFactorCode { get; set; }
        public string ProfileValueSelected { get; set; }
        public int ProfileQuantity { get; set; }
        public double TotalCost { get; set; }
        public string UserId { get; }

        public CreateSizeModelItemRequest(string sizeModelId,
                                          string profileName,
                                          double profileAvgRateAmount,
                                          string sizeModelTypeFactorCode,
                                          string profileValueSelected,
                                          int profileCountValue,
                                          double totalCost,
                                          string userId)
        {
            SizeModelId = sizeModelId;
            ProfileName = profileName;
            ProfileAvgRateAmount = profileAvgRateAmount;
            SizeModelTypeFactorCode = sizeModelTypeFactorCode;
            ProfileValueSelected = profileValueSelected;
            ProfileQuantity = profileCountValue;
            TotalCost = totalCost;
            UserId = userId;
        }
    }
}


