using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeFactorRequest : IRequest<bool>
    {
        public DeactivateSizeModelTypeFactorRequest(string sizeModelTypeFactorId, string userId)
        {
            SizeModelTypeFactorId = sizeModelTypeFactorId;
            UserId = userId;
        }

        public string SizeModelTypeFactorId { get; }
        public string UserId { get; }
    }
}
