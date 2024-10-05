using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelTypeItem
{
    public class ChangeCodeSizeModelTypeItemRequest : ICommand<ResultSet>
    {
        public string SizeModelTypeItemId { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }

        public ChangeCodeSizeModelTypeItemRequest(string sizeModelTypeItemId, string code, string userId)
        {
            Code = code;
            UserId = userId;
            SizeModelTypeItemId = sizeModelTypeItemId;
        }
    }
}
