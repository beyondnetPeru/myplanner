using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeQuantitySizeModelItem
{
    public class ChangeQuantitySizeModelItemRequest : ICommand<ResultSet>
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
