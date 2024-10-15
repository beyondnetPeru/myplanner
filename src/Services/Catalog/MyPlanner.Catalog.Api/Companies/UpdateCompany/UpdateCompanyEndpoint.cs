namespace MyPlanner.Catalog.Api.Companies.UpdateCompany
{
    public record UpdateCompanyRequest(string Name);
    public record UpdateCompanyResponse(bool IsSuccess);

    public class UpdateCompanyEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/companies/{id}", async (string id, [AsParameters] CompanyServices services, [FromBody] UpdateCompanyRequest request) =>
            {
                var command = new UpdateCompanyCommand(id, request.Name);

                var response = await services.Mediator.Send(command);

                return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.UPDATE.Name)
                .Produces<UpdateCompanyResponse>(StatusCodes.Status200OK)
                .Produces<UpdateCompanyResponse>(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .WithSummary(ENDPOINT.UPDATE.Summary)
                .WithDescription(ENDPOINT.UPDATE.Description);
        }
    }
}
