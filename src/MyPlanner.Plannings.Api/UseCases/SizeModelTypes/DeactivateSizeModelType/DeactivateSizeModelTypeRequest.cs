using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.DeactivateSizeModelType
{
    public class DeactivateSizeModelTypeRequest : IRequest<bool>
    {
        public DeactivateSizeModelTypeRequest(string sizeModelTypeId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string UserId { get; }
    }
}
