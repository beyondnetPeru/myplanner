namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModelItem
{
    public class GetSizeModelItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodels/{sizeModelId}/items/{sizeModelItemId}", async
                                                               ([AsParameters] SizeModelService service, string sizeModelId,
                                                               string sizeModelItemId) =>
            {
                var query = new GetSizeModelItemQuery(sizeModelItemId);

                var request = await service.Mediator.Send(query);

                return !request.IsSuccess
                        ? Results.NotFound()
                        : Results.Ok(request);

            }).WithTags(Tags.SizeModels);
        }
    }

}
