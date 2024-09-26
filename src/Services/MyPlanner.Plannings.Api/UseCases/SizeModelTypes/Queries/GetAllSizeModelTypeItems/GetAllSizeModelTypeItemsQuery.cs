using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeItemsQuery : IRequest<IEnumerable<SizeModelTypeItemDto>>
    {
        public GetAllSizeModelTypeItemsQuery(string sizeModelTypeId)
        {
            SizeModelTypeId = sizeModelTypeId;
        }

        public string SizeModelTypeId { get; }
    }
}
