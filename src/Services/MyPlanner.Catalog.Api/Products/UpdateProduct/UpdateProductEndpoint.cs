using MyPlanner.Catalog.Api.UseCases;

namespace MyPlanner.Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/companies/{companyId}/products/{id}", async (string companyId, string id, [AsParameters] ProductServices services, [FromBody] UpdateProductRequest request) =>
            {
                var command = new UpdateProductCommand(companyId, id, request.Name, request.Category, request.Description, request.ImageFile, request.Price);

                var response = await services.Mediator.Send(command);

                return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.UPDATE.Name)
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .Produces<UpdateProductResponse>(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .WithSummary(ENDPOINT.UPDATE.Summary)
                .WithDescription(ENDPOINT.UPDATE.Description);
        }
    }
}
