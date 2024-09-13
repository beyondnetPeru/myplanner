namespace MyPlanner.Plannings.Api.UseCases.SizeModels.GetSizeModelItem
{
    public class GetSizeModelItemController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodels/{sizeModelId}/items/{sizeModelItemId}", async
                                                               (string sizeModelId,
                                                               string sizeModelItemId) =>
            {
                var query = new GetSizeModelItemQuery(sizeModelId, sizeModelItemId);

                var request = await mediator.Send(query);

                return request is null
                        ? Results.NotFound()
                        : Results.Ok(request);

            }).WithTags(Tags.SizeModels);
        }
    }

}
