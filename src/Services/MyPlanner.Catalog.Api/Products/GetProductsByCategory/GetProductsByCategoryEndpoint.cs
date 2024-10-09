using MyPlanner.Catalog.Api.Models;
using MyPlanner.Catalog.Api.UseCases;

namespace MyPlanner.Catalog.Api.Products.GetProductsByCategory
{
    public record GetproductsByCategoryResponse(IEnumerable<Product> products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/companies/{companyId}/products/bycat/{category}", async (string companyId, string category, [AsParameters] ProductServices services) =>
            {
                var query = new GetProductsByCategoryQuery(companyId, category);

                var response = await services.Mediator.Send(query);

                return Results.Ok(response);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.GETCAT.Name)
                .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary(ENDPOINT.GETCAT.Summary)
                .WithDescription(ENDPOINT.GETCAT.Description);
        }
    }
}
