using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelName
{
    public class ChangeNameSizeModelRequest : IRequest<bool>
    {
        public ChangeNameSizeModelRequest(string sizeModelId, string name, string userId)
        {
            SizeModelId = sizeModelId;
            Name = name;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string Name { get; }
        public string UserId { get; }
    }
}
