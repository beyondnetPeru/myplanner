using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelType
{
    public class DeactivateSizeModelTypeCommand : ICommand<ResultSet>
    {
        public DeactivateSizeModelTypeCommand(string sizeModelTypeId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string UserId { get; }
    }
}
