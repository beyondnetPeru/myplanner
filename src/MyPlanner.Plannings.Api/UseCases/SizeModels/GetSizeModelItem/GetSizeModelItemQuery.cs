using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.GetSizeModelItem
{
    public class GetSizeModelItemQuery : IRequest<SizeModelItemDto>
    {

        public GetSizeModelItemQuery(string sizeModelId, string sizeModelItemId)
        {
            SizeModelId = sizeModelId;
            SizeModelItemId = sizeModelItemId;
        }

        public string SizeModelId { get; }
        public string SizeModelItemId { get; }
    }
}
