namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeFactor
{
    public class GetSizeModelTypeItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes/{sizeModelTypeId}/items/{sizeModelTypeFactorId}", async ([AsParameters] SizeModelTypeService service, string sizeModelTypeId, string sizeModelTypeItemId) =>
            {
                var query = new GetSizeModelTypeItemQuery(sizeModelTypeId, sizeModelTypeItemId);

                var result = await service.Mediator.Send(query);

                return result.IsSuccess? Results.Ok(result) : Results.NotFound(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
