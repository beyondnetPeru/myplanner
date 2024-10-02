using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModels
{
    public class GetAllSizeModelsQuery : IRequest<IEnumerable<SizeModelDto>>
    {
        public GetAllSizeModelsQuery(PaginationQuery pagination)
        {
            Pagination = pagination;
        }

        public PaginationQuery Pagination { get; }
    }
}
