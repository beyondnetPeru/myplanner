using BeyondNet.Cqrs.Impl;
using BeyondNet.Cqrs.Interfaces;
using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductCommand(string companyId, string Id) : ICommand<ResultSet>;

    public class DeleteProductCommandHandler : AbstractCommandHandler<DeleteProductCommand, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly ILogger<DeleteProductCommandHandler> logger;

        public DeleteProductCommandHandler(IDocumentSession documentSession, ILogger<DeleteProductCommandHandler> logger): base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ResultSet> HandleCommand(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);

            if (product == null)
            {                
                return ResultSet.Error($"Product with id {request.Id} not found", JsonSerializer.Serialize(product));
            }

            documentSession.Delete(product);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success();
        }
    }
}
