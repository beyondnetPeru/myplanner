using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Products
{
    public record CreateProductCommand : IRequest<CreateProductResult>
    {
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }

    public record CreateProductResult(string Id);

    internal class CreateProductCommandHandler(IDocumentSession documentSession) : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price
            };

            documentSession.Store(product);

            await documentSession.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
