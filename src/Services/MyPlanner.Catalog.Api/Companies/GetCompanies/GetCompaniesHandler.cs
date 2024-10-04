using MyPlanner.Catalog.Api.Models;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Catalog.Api.Companies.GetCompanies
{

    public record GetCompaniesQuery(int? PageNumber, int? PageSize) : IQuery<GetCompaniesResult>;
    public record GetCompaniesResult(IEnumerable<Company> Companies);

    internal class GetCompaniesHandler(IDocumentSession documentSession) : IQueryHandler<GetCompaniesQuery, GetCompaniesResult>
    {
        public async Task<GetCompaniesResult> Handle(GetCompaniesQuery query, CancellationToken cancellationToken)
        {
            var companies = await documentSession.Query<Company>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return new GetCompaniesResult(companies);
        }
    }
}
