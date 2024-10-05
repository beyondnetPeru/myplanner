namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModelItems
{
    public class GetAllSizeModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodels/{sizeModelId}/items", async ([AsParameters] SizeModelService service, string sizeModelId) =>
            {
                var query = new GetAllSizeModelItemQuery(sizeModelId);

                var request = await service.Mediator.Send(query);

                return !request.IsSuccess
                        ? Results.NotFound()
                        : Results.Ok(request);

            }).WithTags(Tags.SizeModels);
        }
    }
}
