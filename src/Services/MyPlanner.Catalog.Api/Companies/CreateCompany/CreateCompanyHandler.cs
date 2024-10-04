using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.CreateCompany
{
    public record CreateCompanyCommad(string Name): ICommand<CreateCompanyResult>;

    public record CreateCompanyResult(string Id);

    public class CreateCompanyCommandValidator: AbstractValidator<CreateCompanyCommad>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    internal class CreateCompanyCommandHandler(IDocumentSession documentSession,
                                               ILogger<CreateCompanyCommandHandler> logger,
                                               IValidator<CreateCompanyCommad> validator) : ICommandHandler<CreateCompanyCommad, CreateCompanyResult>
    {
        public async Task<CreateCompanyResult> Handle(CreateCompanyCommad command, CancellationToken cancellationToken)
        {
            var errors = validator.Validate(command);

            if (errors != null)
            {
                logger.LogError($"Error trying create a company. Errors:{errors.ToString()}");
                return null;
            }

            var company = new Company
            {
                Id = Guid.NewGuid().ToString(),
                Name = command.Name
            };

            documentSession.Store(company);

            await documentSession.SaveChangesAsync(cancellationToken);

            return new CreateCompanyResult(company.Id);
        }
    }
}
