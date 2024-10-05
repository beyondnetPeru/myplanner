using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModel
{
    public class CreateSizeModelRequest : ICommand<ResultSet>
    {
        public string SizeModelTypeId { get; set; }
        public string SizeModelTypeCode { get; set; }
        public string Name { get; set; }
        public ICollection<CreateSizeModelItemRequest> Items { get; set; }
        public string UserId { get; set; }

        public CreateSizeModelRequest(string sizeModelTypeId,
                                      string sizeModelTypeCode,
                                      string name,
                                      ICollection<CreateSizeModelItemRequest> items,
                                      string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeCode = sizeModelTypeCode;
            Name = name;
            Items = items;
            UserId = userId;
        }
    }

    public class CreateSizeModelItemRequest
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

        public CreateSizeModelItemRequest(string sizeModelId,
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