using MyPlanner.Plannings.Shared.Application.Dtos;
using System.Net;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.GetAllSizeModels
{
    public class GetAllSizeModelsController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodels", async ([AsParameters] SizeModelService service, int page = 1, int recordsPerPage = 10) =>
            {
                var pagination = new PaginationDto()
                {
                    Page = page,
                    RecordsPerPage = recordsPerPage
                };

                var query = new GetAllSizeModelsQuery(pagination);

                var request = await service.Mediator.Send(query);


                return request is null
                        ? Results.NotFound()
                        : Results.Ok(request);

            }).WithTags(Tags.SizeModels);
        }
    }
}
