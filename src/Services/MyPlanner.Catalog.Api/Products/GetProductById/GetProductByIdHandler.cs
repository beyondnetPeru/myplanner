using MyPlanner.Catalog.Api.Models;


namespace MyPlanner.Catalog.Api.Products.GetProductById
{
    public record GetProductByIdQuery(string companyId, string Id) : IQuery<ResultSet>;

    public class GetProductByIdQueryHandler : AbstractQueryHandler<GetProductByIdQuery, ResultSet>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<GetProductByIdQueryHandler> logger;

        public GetProductByIdQueryHandler(IDocumentSession documentSession, ILogger<GetProductByIdQueryHandler> logger) : base(logger)
        {
            _documentSession = documentSession;
            this.logger = logger;
        }

        public async override Task<ResultSet> HandleQuery(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _documentSession.Query<Product>().FirstOrDefaultAsync(x => x.CompanyId == request.companyId && x.Id == request.Id, cancellationToken);

            if (response == null)
            {
                return ResultSet.Error($"Product with id {request.Id} not found");
            }

            return ResultSet.Success(response);
        }
    }
}
