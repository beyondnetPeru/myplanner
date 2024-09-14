using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.GetSizeModel
{
    public class GetSizeModelController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodels/{sizeModelId}", async ([AsParameters] SizeModelService service, string sizeModelId) =>
            {
                var query = new GetSizeModelQuery(sizeModelId);

                SizeModelDto request = await mediator.Send(query);

                return request is null
                        ? Results.NotFound()
                        : Results.Ok(request);

            }).WithTags(Tags.SizeModels);
        }
    }
}
