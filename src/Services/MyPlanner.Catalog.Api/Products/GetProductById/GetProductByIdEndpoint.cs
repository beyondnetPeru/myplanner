using MyPlanner.Catalog.Api.Models;
using MyPlanner.Catalog.Api.UseCases;

namespace MyPlanner.Catalog.Api.Products.GetProductById
{
    public record GetProductByIdResponse(Product product);

    public class GetProductIdByQueryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/byid/{id}", async (string id, [AsParameters] ProductServices service)  =>
            {
                var query = new GetProductByIdQuery(id);
                
                var result = await service.Mediator.Send(query);

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            }).Produces<Product>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Product by Id")
            .WithDescription("Get a product by its id.");
        }
    }
}
