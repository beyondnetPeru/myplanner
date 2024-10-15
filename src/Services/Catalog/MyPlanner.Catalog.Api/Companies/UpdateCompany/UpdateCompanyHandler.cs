using BeyondNet.Cqrs.Impl;
using BeyondNet.Cqrs.Interfaces;
using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.UpdateCompany
{
    public record UpdateCompanyCommand(string id, string Name) : ICommand<ResultSet>;

    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }


    public class UpdateCompanyCompanyHandler: AbstractCommandHandler<UpdateCompanyCommand, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly ILogger<UpdateCompanyCompanyHandler> logger;

        public UpdateCompanyCompanyHandler(IDocumentSession documentSession, ILogger<UpdateCompanyCompanyHandler> logger): base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
        }
        public override async Task<ResultSet> HandleCommand(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await documentSession.LoadAsync<Company>(request.id, cancellationToken);

            if (company == null)
            {
               return ResultSet.Error("Company {CompanyId} was not found", request.id);
            }

            company.Name = request.Name;

            documentSession.Update(company);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success();
        }
    } 
}
