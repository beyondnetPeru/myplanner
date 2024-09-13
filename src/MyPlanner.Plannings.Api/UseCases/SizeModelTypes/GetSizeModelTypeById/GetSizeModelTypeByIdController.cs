namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetSizeModelTypeById
{
    public class GetSizeModelTypeByIdController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes/{sizeModelTypeId}", async (string sizeModelTypeId) =>
            {
                var query = new GetSizeModelTypeByIdQuery(sizeModelTypeId);

                var result = await mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
