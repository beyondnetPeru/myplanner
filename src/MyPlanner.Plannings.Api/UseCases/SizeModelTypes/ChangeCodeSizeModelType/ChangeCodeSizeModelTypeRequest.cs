using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ChangeCodeSizeModelType
{
    public class ChangeCodeSizeModelTypeRequest : IRequest<bool>
    {
        public ChangeCodeSizeModelTypeRequest(string sizeModelTypeId, string code, string userId)
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
