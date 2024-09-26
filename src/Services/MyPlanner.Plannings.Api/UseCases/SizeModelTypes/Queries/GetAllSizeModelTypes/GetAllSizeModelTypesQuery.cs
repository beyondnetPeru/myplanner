using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypes
{
    public class GetAllSizeModelTypesQuery : IRequest<IEnumerable<SizeModelTypeDto>>
    {
        public GetAllSizeModelTypesQuery(PaginationDto pagination)
        {
            Pagination = pagination;
        }

        public PaginationDto Pagination { get; }
    }
}
