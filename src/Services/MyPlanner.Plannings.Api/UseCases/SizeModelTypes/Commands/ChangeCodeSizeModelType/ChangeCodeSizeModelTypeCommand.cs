using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelType
{
    public class ChangeCodeSizeModelTypeCommand : ICommand<ResultSet>
    {
        public ChangeCodeSizeModelTypeCommand(string sizeModelTypeId, string code, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            Code = code;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string Code { get; }
        public string UserId { get; }
    }
}
