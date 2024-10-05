namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeItemsController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes/{sizeModelTypeId}/items", async ([AsParameters] SizeModelTypeService service, string sizeModelTypeId) =>
            {
                var query = new GetAllSizeModelTypeItemsQuery(sizeModelTypeId);

                var result = await service.Mediator.Send(query);

                return result.IsSuccess ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
