namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetSizeModelTypeFactor
{
    public class GetSizeModelTypeFactorController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes/{sizeModelTypeId}/factors/{sizeModelTypeFactorId}", async ([AsParameters] SizeModelTypeService service, string sizeModelTypeId, string sizeModelTypeFactorId) =>
            {
                var query = new GetSizeModelTypeFactorQuery(sizeModelTypeId, sizeModelTypeFactorId);

                var result = await mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
