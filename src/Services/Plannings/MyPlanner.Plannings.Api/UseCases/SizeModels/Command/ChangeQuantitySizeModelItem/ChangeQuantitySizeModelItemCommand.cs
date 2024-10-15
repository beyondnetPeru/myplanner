


namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeQuantitySizeModelItem
{
    public class ChangeQuantitySizeModelItemCommand : ICommand<ResultSet>
    {
        public ChangeQuantitySizeModelItemCommand(string sizeModelItemId, int quantity, string userId)
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
