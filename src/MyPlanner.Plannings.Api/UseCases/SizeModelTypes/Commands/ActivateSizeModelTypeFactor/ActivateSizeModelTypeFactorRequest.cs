
using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeFactorRequest : IRequest<bool>
    {
        public ActivateSizeModelTypeFactorRequest(string sizeModelTypeFactorId, string userId)
        {
            SizeModelTypeFactorId = sizeModelTypeFactorId;
            UserId = userId;
        }

        public string SizeModelTypeFactorId { get; }
        public string UserId { get; }
    }
}
