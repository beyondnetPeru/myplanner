using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypes
{
    public class GetAllSizeModelTypesController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes", async ([AsParameters] SizeModelTypeService service, int page = 1, int recordsPerPage = 10) =>
            {

                var pagination = new PaginationDto()
                {
                    Page = page,
                    RecordsPerPage = recordsPerPage
                };

                var query = new GetAllSizeModelTypesQuery(pagination);

                var result = await service.Mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
