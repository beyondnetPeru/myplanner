namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeFactorsController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes/{sizeModelTypeId}/factors", async ([AsParameters] SizeModelTypeService service, string sizeModelTypeId) =>
            {
                var query = new GetAllSizeModelTypeFactorQuery(sizeModelTypeId);

                var result = await service.Mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
