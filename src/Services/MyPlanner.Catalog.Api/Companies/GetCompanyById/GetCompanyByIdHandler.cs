using MyPlanner.Catalog.Api.Models;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Catalog.Api.Companies.GetCompanyById
{
    public record GetCompanyByIdQuery(string Id) : IQuery<GetCompanyByIdResult>;

    public record GetCompanyByIdResult(Company Company);

    internal class GetCompanyByIdQueryHandler : IQueryHandler<GetCompanyByIdQuery, GetCompanyByIdResult>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<GetCompanyByIdQueryHandler> logger;

        public GetCompanyByIdQueryHandler(IDocumentSession documentSession, ILogger<GetCompanyByIdQueryHandler> logger)
        {
            _documentSession = documentSession;
            this.logger = logger;
        }

        public async Task<GetCompanyByIdResult> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _documentSession.Query<Company>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            if (company == null) {
                logger.LogWarning("Company with id {Id} not found", request.Id);
                return null;
            }

            return new GetCompanyByIdResult(company);
        }
    }
}
