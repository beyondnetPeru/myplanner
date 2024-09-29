using Microsoft.AspNetCore.Mvc;
using MyPlanner.Catalog.Api.Models;
using MyPlanner.Catalog.Api.UseCases;
using MyPlanner.Shared.Infrastructure.Marten.Pagination;

namespace MyPlanner.Catalog.Api.Products.GetProducts
{
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/", async ([FromBody] GetPaginationRequest paginationRequest, [AsParameters] ProductServices services) =>
            {
                var query = paginationRequest.Adapt<GetProductsQuery>();

                var result = await services.Mediator.Send(query);

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);
            }).WithName("GetProducts")
            .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get all products.");
        }
    }
}
