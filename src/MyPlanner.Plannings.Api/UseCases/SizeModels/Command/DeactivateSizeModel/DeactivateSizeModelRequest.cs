using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModel
{
    public class DeactivateSizeModelRequest : IRequest<bool>
    {
        public DeactivateSizeModelRequest(string sizeModelId, string userId)
        {
            SizeModelId = sizeModelId;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string UserId { get; }
    }
}
