namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeTotalCostSizeModelItem
{
    public class ChangeTotalCostSizeModelItemRequest : IRequest<bool>
    {
        public string SizeModelItemId { get; set; }
        public double TotalCost { get; set; }
        public string UserId { get; set; }

        public ChangeTotalCostSizeModelItemRequest(string sizeModelItemId, double totalCost, string userId)
        {
            SizeModelItemId = sizeModelItemId;
            TotalCost = totalCost;
            UserId = userId;
        }

    }
}
