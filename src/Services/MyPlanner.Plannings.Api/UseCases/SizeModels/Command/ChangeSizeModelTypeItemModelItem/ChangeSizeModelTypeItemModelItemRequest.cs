using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelTypeItem
{
    public class ChangeSizeModelTypeItemModelItemRequest : ICommand<ResultSet>
    {
        public string SizeModelItemId { get; set; }
        public string SizeModelItemTypeId { get; set; }
        public string SizeModelItemTypeCode { get; set; }
        public string UserId { get; set; }

        public ChangeSizeModelTypeItemModelItemRequest(string sizeModelItemId, string sizeModelItemTypeId, string sizeModelItemTypeCode, string userId)
        {
            SizeModelItemTypeId = sizeModelItemTypeId;
            SizeModelItemId = sizeModelItemId;
            UserId = userId;
            SizeModelItemTypeCode = sizeModelItemTypeCode;
        }
    }
}
