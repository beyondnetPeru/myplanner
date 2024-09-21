namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeQuantitySizeModelItem
{
    public class ChangeQuantitySizeModelItemRequest : IRequest<bool>
    {
        public ChangeQuantitySizeModelItemRequest(string sizeModelItemId, int quantity, string userId)
        {
            SizeModelItemId = sizeModelItemId;
            Quantity = quantity;
            UserId = userId;
        }

        public string SizeModelItemId { get; }
        public int Quantity { get; }
        public string UserId { get; }
    }
}
