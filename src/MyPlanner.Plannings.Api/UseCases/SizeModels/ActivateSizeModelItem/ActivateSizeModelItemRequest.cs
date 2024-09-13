using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.ActivateSizeModelItem
{
    public class ActivateSizeModelItemRequest : IRequest<bool>
    {
        public ActivateSizeModelItemRequest(string sizeModelId, string sizeModelItemId, string userId)
        {
            SizeModelId = sizeModelId;
            SizeModelItemId = sizeModelItemId;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string SizeModelItemId { get; }
        public string UserId { get; }
    }
}
