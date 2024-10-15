using MyPlanner.Catalog.Api.Products;

namespace MyPlanner.Catalog.Api.UseCases.CreateProduct
{
    public record CreateProductRequest(string companyId, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record class CreateProductResponse(string Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/companies/{companyId}/products/", async (string companyId, [AsParameters] ProductServices services, [FromBody] CreateProductRequest createProductRequest) =>
            {
                var command = createProductRequest.Adapt<CreateProductCommand>();
                command.CompanyId = companyId;

                var response = await services.Mediator.Send(command);

                return Results.Ok(response);

            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.CREATE.Name)                
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary(ENDPOINT.CREATE.Summary)
                .WithDescription(ENDPOINT.CREATE.Description);            
        }
    }
}
