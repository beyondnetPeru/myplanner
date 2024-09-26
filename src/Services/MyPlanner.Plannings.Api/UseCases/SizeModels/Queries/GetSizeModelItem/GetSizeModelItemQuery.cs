using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModelItem
{
    public class GetSizeModelItemQuery : IRequest<SizeModelItemDto>
    {

        public GetSizeModelItemQuery(string sizeModelItemId)
        {
            SizeModelItemId = sizeModelItemId;
        }

        public string SizeModelItemId { get; }
    }
}
