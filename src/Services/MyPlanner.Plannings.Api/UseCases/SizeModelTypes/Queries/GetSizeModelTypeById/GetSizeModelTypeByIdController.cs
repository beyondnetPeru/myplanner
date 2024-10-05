namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeById
{
    public class GetSizeModelTypeByIdController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes/{sizeModelTypeId}", async ([AsParameters] SizeModelTypeService service, string sizeModelTypeId) =>
            {
                var query = new GetSizeModelTypeByIdQuery(sizeModelTypeId);

                var result = await service.Mediator.Send(query);

                return result.IsSuccess? Results.Ok(result) : Results.NotFound(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
