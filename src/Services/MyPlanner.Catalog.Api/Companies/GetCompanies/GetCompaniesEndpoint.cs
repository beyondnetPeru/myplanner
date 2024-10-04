using MyPlanner.Catalog.Api.Models;
using MyPlanner.Shared.Infrastructure.Marten.Pagination;

namespace MyPlanner.Catalog.Api.Companies.GetCompanies
{
    public record GetCompanyResponse(IEnumerable<Company> Companies);

    public class GetCompaniesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/companies/", async ([FromBody] GetPaginationRequest paginationRequest, [AsParameters] CompanyServices services) =>
            {
                var query = paginationRequest.Adapt<GetCompaniesQuery>();

                var result = await services.Mediator.Send(query);

                var response = result.Adapt<GetCompanyResponse>();

                return Results.Ok(response);
            }).WithName("GetCompanies")
            .Produces<IEnumerable<Company>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Companies")
            .WithDescription("Get all companies.");
        }
    }
}
