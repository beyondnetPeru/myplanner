namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeFactorsController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes/{sizeModelTypeId}/factors", async (string sizeModelTypeId) =>
            {
                var query = new GetAllSizeModelTypeFactorQuery(sizeModelTypeId);

                var result = await mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
