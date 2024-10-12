using BeyondNet.Cqrs.Impl;
using BeyondNet.Cqrs.Interfaces;
using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.GetCompanies
{
    public record GetCompaniesQuery(int? PageNumber, int? PageSize) : IQuery<ResultSet>;

    public class GetCompaniesQueryHandler : AbstractQueryHandler<GetCompaniesQuery, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly ILogger<GetCompaniesQueryHandler> logger;

        public GetCompaniesQueryHandler(IDocumentSession documentSession, ILogger<GetCompaniesQueryHandler> logger) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ResultSet> HandleQuery(GetCompaniesQuery query, CancellationToken cancellationToken)
        {
            var companies = await documentSession.Query<Company>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return ResultSet.Success(companies);
        }
    }
}
