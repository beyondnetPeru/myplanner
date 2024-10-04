using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.UpdateCompany
{
    public record UpdateCompanyCommand(string id, string Name) : ICommand<UpdateCommandResponse>;
    public record UpdateCommandResponse(bool IsSuccess);

    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }


    internal class UpdateCompanyCompanyHandler(IDocumentSession documentSession, ILogger<UpdateCompanyCompanyHandler> logger) : ICommandHandler<UpdateCompanyCommand, UpdateCommandResponse>
    {
        public async Task<UpdateCommandResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await documentSession.LoadAsync<Company>(request.id, cancellationToken);

            if (company == null)
            {
                logger.LogError("Company {CompanyId} was not found", request.id);
                return new UpdateCommandResponse(false);
            }

            company.Name = request.Name;

            documentSession.Update(company);

            await documentSession.SaveChangesAsync(cancellationToken);

            return new UpdateCommandResponse(true);
        }
    } 
}
