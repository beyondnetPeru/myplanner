using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModel
{
    public class ActivateSizeModelRequest : ICommand<ResultSet>
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
