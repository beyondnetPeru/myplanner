using MyPlanner.Catalog.Api.Models;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Catalog.Api.Products.GetProductById
{
    public record GetProductByIdQuery(string companyId, string Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<GetProductByIdQueryHandler> logger;

        public GetProductByIdQueryHandler(IDocumentSession documentSession, ILogger<GetProductByIdQueryHandler> logger)
        {
            _documentSession = documentSession;
            this.logger = logger;
        }

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _documentSession.Query<Product>().FirstOrDefaultAsync(x => x.CompanyId == request.companyId && x.Id == request.Id, cancellationToken);

            if (product == null)
            {
                logger.LogWarning("Product with id {Id} not found", request.Id);
                return null;
            }

            return new GetProductByIdResult(product);
        }
    }
}
