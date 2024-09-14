namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeFactor
{
    public class GetSizeModelTypeFactorController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes/{sizeModelTypeId}/factors/{sizeModelTypeFactorId}", async ([AsParameters] SizeModelTypeService service, string sizeModelTypeId, string sizeModelTypeFactorId) =>
            {
                var query = new GetSizeModelTypeFactorQuery(sizeModelTypeId, sizeModelTypeFactorId);

                var result = await service.Mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
