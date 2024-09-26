using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModelItems
{
    public class GetAllSizeModelItemQuery : IRequest<IEnumerable<SizeModelItemDto>>
    {
        public GetAllSizeModelItemQuery(string sizeModelId)
        {
            SizeModelId = sizeModelId;
        }

        public string SizeModelId { get; }
    }
}
