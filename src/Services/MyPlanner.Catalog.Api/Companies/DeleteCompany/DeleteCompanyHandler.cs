using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.DeleteCompany
{
    public record DeleteCompanyCommand(string Id) : ICommand<DeleteCompanyCommandResponse>;

    public record DeleteCompanyCommandResponse(bool IsSuccess);

    internal class DeleteCompanyCommandHandler(IDocumentSession documentSession, ILogger<DeleteCompanyCommandHandler> logger) : ICommandHandler<DeleteCompanyCommand, DeleteCompanyCommandResponse>
    {
        public async Task<DeleteCompanyCommandResponse> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await documentSession.LoadAsync<Company>(request.Id, cancellationToken);
            if (company == null)
            {
                logger.LogWarning("Company with id {id} not found", request.Id);
                return new DeleteCompanyCommandResponse(false);
            }

            documentSession.Delete(company);

            await documentSession.SaveChangesAsync(cancellationToken);

            return new DeleteCompanyCommandResponse(true);
        }
    }
}
