using Microsoft.AspNetCore.Mvc;
using MyPlanner.Catalog.Api.Products;

namespace MyPlanner.Catalog.Api.UseCases.CreateProduct
{
    public record CreateProductRequest(string companyId, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record class CreateProductResponse(string Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/companies/{companyId}/products/", async ([AsParameters] ProductServices services, [FromBody] CreateProductRequest createProductRequest) =>
            {
                var command = createProductRequest.Adapt<CreateProductCommand>();

                var result = await services.Mediator.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);

            }).WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create a new product.");
        }
    }
}
