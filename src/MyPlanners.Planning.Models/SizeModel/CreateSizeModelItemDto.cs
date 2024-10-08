namespace MyPlanner.Plannings.Models.SizeModel
{
    public class CreateSizeModelItemDto : AbstractUserDto
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
                                      string userId) : base(userId)

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
        }
    }
}
