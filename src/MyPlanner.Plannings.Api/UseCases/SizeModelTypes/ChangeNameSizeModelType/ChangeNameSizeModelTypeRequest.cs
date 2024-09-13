using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeRequest : IRequest<bool>
    {
        public ChangeNameSizeModelTypeRequest(string id, string name, string userId)
        {
            Id = id;
            Name = name;
            UserId = userId;
        }

        public string Id { get; }
        public string Name { get; }
        public string UserId { get; }
    }
}
