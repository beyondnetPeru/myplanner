namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypes
{
    public class GetAllSizeModelTypesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sizemodeltypes", async ([AsParameters] SizeModelTypeService service, int page = 1, int recordsPerPage = 10) =>
            {

                var pagination = new PaginationQuery(page, recordsPerPage);

                var query = new GetAllSizeModelTypesQuery(pagination);

                var result = await service.Mediator.Send(query);

                return result.IsSuccess ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
