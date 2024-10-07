using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelTypeItem
{
    public class DeleteSizeModelTypeItemCommand : ICommand<ResultSet>
    {
        public string SizeModelTypeItemId { get; set; }
        public string UserId { get; set; }

        public DeleteSizeModelTypeItemCommand(string sizeModelTypeItemId, string userId)
        {
            SizeModelTypeItemId = sizeModelTypeItemId;
            UserId = userId;
        }

    }
}
