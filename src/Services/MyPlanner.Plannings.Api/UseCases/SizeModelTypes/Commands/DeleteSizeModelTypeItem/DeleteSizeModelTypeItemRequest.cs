using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelTypeItem
{
    public class DeleteSizeModelTypeItemRequest : ICommand<ResultSet>
    {
        public string SizeModelTypeItemId { get; set; }
        public string UserId { get; set; }

        public DeleteSizeModelTypeItemRequest(string sizeModelTypeItemId, string userId)
        {
            SizeModelTypeItemId = sizeModelTypeItemId;
            UserId = userId;
        }

    }
}
