using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeItemCommand : ICommand<ResultSet>
    {
        public DeactivateSizeModelTypeItemCommand(string sizeModelTypeItemId, string userId)
        {
            SizeModelTypeItemId = sizeModelTypeItemId;
            UserId = userId;
        }

        public string SizeModelTypeItemId { get; }
        public string UserId { get; }
    }
}
