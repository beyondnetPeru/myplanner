using Carter;
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Catalog.Api.Dtos;

namespace MyPlanner.Catalog.Api.UseCases.CreateProduct
{
    public class CreateProductController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("products", async (ProductServices services, [FromBody] CreateProductDto createProductDto) =>
            {
                var request = new CreateProductRequest(createProductDto.Name,
                                  createProductDto.Category,
                                  createProductDto.Description,
                                  createProductDto.ImageFile,
                                  createProductDto.Price);

                var result = await services.Mediator.Send(request);

                return result ? Results.Ok() : Results.BadRequest();
            });
        }
    }
}
