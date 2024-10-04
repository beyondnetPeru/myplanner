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

                var result = await service.Mediator.Send(query);

                var response = result.Adapt<GetCompanyByIdResponse>();

                return Results.Ok(response);
            })
                .WithTags("Companies")
                .Produces<Company>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Company by Id")
                .WithDescription("Get a company by its id.");
        }
    }
}
