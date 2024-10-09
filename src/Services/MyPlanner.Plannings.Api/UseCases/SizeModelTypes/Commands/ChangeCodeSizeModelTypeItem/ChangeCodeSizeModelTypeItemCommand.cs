using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelTypeItem
{
    public class ChangeCodeSizeModelTypeItemCommand : ICommand<ResultSet>
    {
        public string SizeModelTypeId { get; }
        public string SizeModelTypeItemId { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }

        public ChangeCodeSizeModelTypeItemCommand(string sizeModelTypeId, string sizeModelTypeItemId, string code, string userId)
        {
            Code = code;
            UserId = userId;
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeItemId = sizeModelTypeItemId;
        }
    }
}
