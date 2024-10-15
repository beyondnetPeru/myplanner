
using MyPlanner.Catalog.Api.UseCases;

namespace MyPlanner.Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/companies/{companyId}/products/{id}", async (string companyId, string id, [AsParameters] ProductServices services) =>
            {
                var command = new DeleteProductCommand(companyId, id);

                var response = await services.Mediator.Send(command);

                return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.DELETE.Name)
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError)
                .ProducesValidationProblem()
                .WithSummary(ENDPOINT.DELETE.Summary)
                .WithDescription(ENDPOINT.DELETE.Description);
        }
    }
}
