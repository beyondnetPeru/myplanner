using MyPlanner.Catalog.Api.Models;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductCommand(string companyId, string Id) : ICommand<DeleteProductCommandResponse>;

    public record DeleteProductCommandResponse(bool IsSuccess);

    internal class DeleteProductCommandHandler(IDocumentSession documentSession, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductCommandResponse>
    {
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
            {
                logger.LogWarning("Product with id {id} not found", request.Id);
                return new DeleteProductCommandResponse(false);
            }

            documentSession.Delete(product);
            await documentSession.SaveChangesAsync(cancellationToken);

            return new DeleteProductCommandResponse(true);
        }
    }
}
