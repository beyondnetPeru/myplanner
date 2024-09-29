
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Catalog.Api.UseCases;

namespace MyPlanner.Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductRequest(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/", async ([AsParameters] ProductServices services, [FromBody] UpdateProductRequest request) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var response = await services.Mediator.Send(command);

                return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
            }).WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .Produces<UpdateProductResponse>(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithSummary("Update a product");
        }
    }
}
