using MyPlanner.Catalog.Api.Models;
using MyPlanner.Catalog.Api.UseCases;

namespace MyPlanner.Catalog.Api.Products.GetProductById
{
    
    public record GetProductByIdResponse(Product product);

    public class GetProductIdByQueryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/companies/{companyId}/products/byid/{id}", async (string companyId, string id, [AsParameters] ProductServices service)  =>
            {
                var query = new GetProductByIdQuery(companyId, id);
                
                var response = await service.Mediator.Send(query);

                return Results.Ok(response);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.GET.Name)
                .Produces<Product>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary(ENDPOINT.GET.Summary)
                .WithDescription(ENDPOINT.GET.Description);
        }
    }
}
