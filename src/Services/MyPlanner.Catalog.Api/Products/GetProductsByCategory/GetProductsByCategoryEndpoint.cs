using MyPlanner.Catalog.Api.Models;
using MyPlanner.Catalog.Api.UseCases;

namespace MyPlanner.Catalog.Api.Products.GetProductsByCategory
{
    public record GetproductsByCategoryResponse(IEnumerable<Product> products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/bycat/{category}", async (string category, [AsParameters] ProductServices services) =>
            {
                var query = new GetProductsByCategoryQuery(category);

                var result = await services.Mediator.Send(query);

                var response = result.Adapt<GetproductsByCategoryResponse>();

                return Results.Ok(response);
            });
        }
    }
}
