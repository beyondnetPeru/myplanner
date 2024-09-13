using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.GetAllSizeModelItems
{
    public class GetAllSizeModelItemController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodels/{sizeModelId}/items", async (string sizeModelId) =>
            {
                var query = new GetAllSizeModelItemQuery(sizeModelId);

                var request = await mediator.Send(query);

                return request is null
                        ? Results.NotFound()
                        : Results.Ok(request);

            }).WithTags(Tags.SizeModels);
        }
    }
}
