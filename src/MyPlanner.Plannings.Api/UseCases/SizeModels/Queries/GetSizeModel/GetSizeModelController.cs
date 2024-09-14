using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModel
{
    public class GetSizeModelController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodels/{sizeModelId}", async ([AsParameters] SizeModelService service, string sizeModelId) =>
            {
                var query = new GetSizeModelQuery(sizeModelId);

                SizeModelDto request = await service.Mediator.Send(query);

                return request is null
                        ? Results.NotFound()
                        : Results.Ok(request);

            }).WithTags(Tags.SizeModels);
        }
    }
}
