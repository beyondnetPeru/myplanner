using MyPlanner.Catalog.Api.Models;


namespace MyPlanner.Catalog.Api.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string companyId, string Category) : IQuery<ResultSet>;

    public class GetProductsByCategoryHandler : AbstractQueryHandler<GetProductsByCategoryQuery, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly ILogger<GetProductsByCategoryHandler> logger;

        public GetProductsByCategoryHandler(IDocumentSession documentSession, ILogger<GetProductsByCategoryHandler> logger):base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ResultSet> HandleQuery(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await documentSession.Query<Product>()
                .Where(x => x.CompanyId == request.companyId && x.Category.Contains(request.Category))                
                .ToListAsync(cancellationToken);

            return ResultSet.Success(products);
        }
    }
}
