using BeyondNet.Cqrs.Impl;
using BeyondNet.Cqrs.Interfaces;
using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.GetCompanyById
{
    public record GetCompanyByIdQuery(string Id) : IQuery<ResultSet>;


    public class GetCompanyByIdQueryHandler : AbstractQueryHandler<GetCompanyByIdQuery, ResultSet>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<GetCompanyByIdQueryHandler> logger;

        public GetCompanyByIdQueryHandler(IDocumentSession documentSession, ILogger<GetCompanyByIdQueryHandler> logger):base(logger)
        {
            _documentSession = documentSession;
            this.logger = logger;
        }

        public override async Task<ResultSet> HandleQuery(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _documentSession.Query<Company>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            if (company == null) {
                return ResultSet.Error($"Company with id {request.Id} not found");
            }

            return ResultSet.Success(company);
        }
    }
}
