namespace MyPlanner.Catalog.Api.Companies.CreateCompany
{
    public record CreateCompanyRequest(string Name);
    public class CreateCompanyEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/companies", async ([AsParameters] CompanyServices services, [FromBody] CreateCompanyRequest createCompanyRequest) =>
            {
                var command = createCompanyRequest.Adapt<CreateCompanyCommad>();

                var response = await services.Mediator.Send(command);

                return Results.Ok(response);

            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.CREATE.Name)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary(ENDPOINT.CREATE.Summary)
                .WithDescription(ENDPOINT.CREATE.Description);
        }
    }
}
