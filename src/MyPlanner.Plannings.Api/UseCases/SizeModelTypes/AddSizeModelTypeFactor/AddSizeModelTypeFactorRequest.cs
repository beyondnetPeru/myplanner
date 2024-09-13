using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.AddSizeModelTypeFactor
{
    public class AddSizeModelTypeFactorRequest : IRequest<bool>
    {
        public AddSizeModelTypeFactorRequest(string sizeModelId, string code, string name, string userId)
        {
            SizeModelId = sizeModelId;
            Code = code;
            Name = name;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string Code { get; }
        public string Name { get; }
        public string UserId { get; }
    }
}
