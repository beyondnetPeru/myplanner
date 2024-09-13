using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.ChangeSizeModelName
{
    public class ChangeSizeModelNameRequest : IRequest<bool>
    {
        public ChangeSizeModelNameRequest(string sizeModelId, string name, string userId)
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
