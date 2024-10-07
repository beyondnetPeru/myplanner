namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModel
{
    public class GetSizeModelEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodels/{sizeModelId}", async ([AsParameters] SizeModelService service, string sizeModelId) =>
            {
                var query = new GetSizeModelQuery(sizeModelId);

                var request = await service.Mediator.Send(query);

                return !request.IsSuccess
                        ? Results.NotFound()
                        : Results.Ok(request);

            }).WithTags(Tags.SizeModels);
        }
    }
}
