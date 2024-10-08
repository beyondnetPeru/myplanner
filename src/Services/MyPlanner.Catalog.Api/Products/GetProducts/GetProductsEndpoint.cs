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
            app.MapGet("/companies/{companyId}/products/", async (string companyId, [FromBody] GetProductsRequest getProductsRequest, [AsParameters] ProductServices services) =>
            {
                var query = new GetProductsQuery(getProductsRequest.PageNumber, getProductsRequest.PageSize, companyId);

                var result = await services.Mediator.Send(query);

                return Results.Ok(result);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.LIST.Name)
                .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)   
                .WithSummary(ENDPOINT.LIST.Summary)
                .WithDescription(ENDPOINT.LIST.Description);
        }
    }
}
