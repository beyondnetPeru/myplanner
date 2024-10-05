using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeItemRequest : ICommand<ResultSet>
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
