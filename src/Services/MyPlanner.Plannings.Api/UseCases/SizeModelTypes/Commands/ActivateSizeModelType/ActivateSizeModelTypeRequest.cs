using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelType
{
    public class ActivateSizeModelTypeRequest : ICommand<ResultSet>
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
