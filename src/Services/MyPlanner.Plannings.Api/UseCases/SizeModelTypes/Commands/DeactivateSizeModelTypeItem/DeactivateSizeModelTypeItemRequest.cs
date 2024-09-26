using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeItemRequest : IRequest<bool>
    {
        public DeactivateSizeModelTypeItemRequest(string sizeModelTypeItemId, string userId)
        {
            SizeModelTypeItemId = sizeModelTypeItemId;
            UserId = userId;
        }

        public string SizeModelTypeItemId { get; }
        public string UserId { get; }
    }
}
