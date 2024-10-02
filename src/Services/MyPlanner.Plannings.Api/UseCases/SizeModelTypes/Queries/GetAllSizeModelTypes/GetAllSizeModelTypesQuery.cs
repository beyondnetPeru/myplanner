using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypes
{
    public class GetAllSizeModelTypesQuery : IRequest<IEnumerable<SizeModelTypeDto>>
    {
        public GetAllSizeModelTypesQuery(PaginationQuery pagination)
        {
            Pagination = pagination;
        }

        public PaginationQuery Pagination { get; }
    }
}
