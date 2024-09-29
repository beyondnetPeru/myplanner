using MyPlanner.Catalog.Api.Models;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductCommand(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductCommandResponse>;

    public record UpdateProductCommandResponse(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
        }

        internal class UpdateProductCommandHandler(IDocumentSession documentSession, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductCommandResponse>
        {
            public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);

                if (product == null)
                {
                    logger.LogError("Product {ProductId} was not found", request.Id);
                    return new UpdateProductCommandResponse(false);
                }

                product.Name = request.Name;
                product.Category = request.Category;
                product.Description = request.Description;
                product.ImageFile = request.ImageFile;
                product.Price = request.Price;

                documentSession.Update(product);

                await documentSession.SaveChangesAsync(cancellationToken);

                return new UpdateProductCommandResponse(true);
            }
        }
    }
}
