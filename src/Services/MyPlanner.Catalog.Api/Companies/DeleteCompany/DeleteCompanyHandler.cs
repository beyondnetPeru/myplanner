using BeyondNet.Cqrs.Impl;
using BeyondNet.Cqrs.Interfaces;
using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.DeleteCompany
{
    public record DeleteCompanyCommand(string Id) : ICommand<ResultSet>;

    public class DeleteCompanyCommandHandler : AbstractCommandHandler<DeleteCompanyCommand, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly ILogger<DeleteCompanyCommandHandler> logger;

        public DeleteCompanyCommandHandler(IDocumentSession documentSession, ILogger<DeleteCompanyCommandHandler> logger) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ResultSet> HandleCommand(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await documentSession.LoadAsync<Company>(request.Id, cancellationToken);
            
            if (company == null)
            {
                return ResultSet.Error("Company with id {id} not found.", request.Id);
            }

            documentSession.Delete(company);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success("Company deleted successfully.");
        }
    }
}
