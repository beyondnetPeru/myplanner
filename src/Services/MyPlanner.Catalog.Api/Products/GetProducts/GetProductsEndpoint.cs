using MyPlanner.Catalog.Api.Models;
using MyPlanner.Catalog.Api.UseCases;

namespace MyPlanner.Catalog.Api.Products.GetProducts
{
    public record GetProductsRequest(int? PageNumber, int? PageSize, string CompanyId); 
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/companies/{companyId}/products/", async ([FromBody] GetProductsRequest getProductsRequest, [AsParameters] ProductServices services) =>
            {
                var query = getProductsRequest.Adapt<GetProductsQuery>();
                
                var result = await services.Mediator.Send(query);

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);
            })
                .WithTags("Products")
                .WithName("GetProducts")
                .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get all products.");
        }
    }
}
