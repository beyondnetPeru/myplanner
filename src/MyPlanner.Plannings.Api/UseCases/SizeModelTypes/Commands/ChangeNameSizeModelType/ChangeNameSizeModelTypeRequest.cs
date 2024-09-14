using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeRequest : IRequest<bool>
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
