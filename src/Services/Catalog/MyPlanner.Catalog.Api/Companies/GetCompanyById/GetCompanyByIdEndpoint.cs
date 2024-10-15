using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.GetCompanyById
{
    public record GetCompanyByIdResponse(Company company);

    public class GetCompanyByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/companies/byid/{id}", async (string id, [AsParameters] CompanyServices service) =>
            {
                var query = new GetCompanyByIdQuery(id);

                var response = await service.Mediator.Send(query);

                return Results.Ok(response);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.GET.Name)
                .Produces<Company>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary(ENDPOINT.GET.Summary)
                .WithDescription(ENDPOINT.GET.Description);
        }
    }
}
