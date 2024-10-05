namespace MyPlanner.Catalog.Api.Companies.CreateCompany
{
    public record CreateCompanyRequest(string Name);
    public record class CreateCompanyResponse(string Id);

    public class CreateCompanyEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/companies", async ([AsParameters] CompanyServices services, [FromBody] CreateCompanyRequest createCompanyRequest) =>
            {
                var command = createCompanyRequest.Adapt<CreateCompanyCommad>();

                var result = await services.Mediator.Send(command);

                var response = result.Adapt<CreateCompanyResponse>();

                return Results.Created($"/companies/{response.Id}", response);

            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.CREATE.Name)
                .Produces<CreateCompanyResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary(ENDPOINT.CREATE.Summary)
                .WithDescription(ENDPOINT.CREATE.Description);
        }
    }
}
