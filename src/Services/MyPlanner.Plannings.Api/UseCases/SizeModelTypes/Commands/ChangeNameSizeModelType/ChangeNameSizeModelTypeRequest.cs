using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeRequest : ICommand<ResultSet>
    {
        public ChangeNameSizeModelTypeRequest(string sizeModelTypeId, string name, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            Name = name;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string Name { get; }
        public string UserId { get; }
    }
}
