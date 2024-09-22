using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModel
{
    public class CreateSizeModelRequest : IRequest<bool>
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
        public int FactorSelected { get; set; }
        public string ProfileName { get; set; }
        public int ProfileAvgRateSymbol { get; set; }
        public double ProfileAvgRateValue { get; set; }
        public string SizeModelTypeSelected { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public bool IsStandard { get; set; }
        public string UserId { get; }

        public CreateSizeModelItemRequest(string sizeModelId,
                                          string sizeModelTypeItemId,
                                          int factorSelected,
                                          string profileName,
                                          int profileAvgRateSymbol,
                                          double profileAvgRateValue,
                                          string sizeModelTypeSelected,
                                          int quantity,
                                          double totalCost,
                                          bool isStandard,
                                          string userId)
        {
            SizeModelId = sizeModelId;
            SizeModelTypeItemId = sizeModelTypeItemId;
            FactorSelected = factorSelected;
            ProfileName = profileName;
            ProfileAvgRateSymbol = profileAvgRateSymbol;
            ProfileAvgRateValue = profileAvgRateValue;
            SizeModelTypeSelected = sizeModelTypeSelected;
            Quantity = quantity;
            TotalCost = totalCost;
            IsStandard = isStandard;
            UserId = userId;
        }


    }

}