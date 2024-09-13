using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.ActivateSizeModel
{
    public class ActivateSizeModelRequest : IRequest<bool>
    {
        public ActivateSizeModelRequest(string sizeModelId, string userId)
        {
            SizeModelId = sizeModelId;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string UserId { get; }
    }
}
