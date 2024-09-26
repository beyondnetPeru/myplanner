using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModels
{
    public class GetAllSizeModelsQuery : IRequest<IEnumerable<SizeModelDto>>
    {
        public GetAllSizeModelsQuery(PaginationDto pagination)
        {
            Pagination = pagination;
        }

        public PaginationDto Pagination { get; }
    }
}
