namespace MyPlanner.Catalog.Api.Companies.DeleteCompany
{
    public record DeleteCompanyResponse(bool IsSuccess);

    public class DeleteCompanyEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/companies/{id}", async (string id, [AsParameters] CompanyServices services) =>
            {
                var command = new DeleteCompanyCommand(id);

                var result = await services.Mediator.Send(command);

                var response = result.Adapt<DeleteCompanyResponse>();

                return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);

            })
                .WithTags("Companies")
                .WithName("DeleteCompany")
                .Produces<DeleteCompanyResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError)
                .ProducesValidationProblem()
                .WithSummary("Delete a company");
        }
    }
}
