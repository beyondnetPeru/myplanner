namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetSizeModelTypeByCode
{
    public class GetSizeModelTypeByCodeController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes/{sizeModelTypeCode}", async ([AsParameters] SizeModelTypeService service, string sizeModelTypeCode) =>
            {
                var query = new GetSizeModelTypeByCodeQuery(sizeModelTypeCode);

                var result = await service.Mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.SizeModelTypes);
        }

    }
}
