using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ActivateSizeModelType
{
    public class ActivateSizeModelTypeRequest : IRequest<bool>
    {
        public ActivateSizeModelTypeRequest(string sizeModelTypeId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string UserId { get; }
    }
}
