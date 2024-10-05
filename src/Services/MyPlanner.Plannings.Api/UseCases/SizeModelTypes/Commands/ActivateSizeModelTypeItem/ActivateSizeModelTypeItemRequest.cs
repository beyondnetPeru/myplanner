using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeItemRequest : ICommand<ResultSet>
    {
        public ActivateSizeModelTypeItemRequest(string sizeModelTypeItemId, string userId)
        {
            SizeModelTypeItemId = sizeModelTypeItemId;
            UserId = userId;
        }

        public string SizeModelTypeItemId { get; }
        public string UserId { get; }
    }
}
