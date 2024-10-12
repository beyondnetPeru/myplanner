using BeyondNet.Cqrs.Impl;
using BeyondNet.Cqrs.Interfaces;
using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.CreateCompany
{
    public record CreateCompanyCommad(string Name): ICommand<ResultSet>;

    public class CreateCompanyCommandValidator: AbstractValidator<CreateCompanyCommad>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class CreateCompanyCommandHandler : AbstractCommandHandler<CreateCompanyCommad, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly ILogger<CreateCompanyCommandHandler> logger;
        private readonly IValidator<CreateCompanyCommad> validator;

        public CreateCompanyCommandHandler(IDocumentSession documentSession,
                                               ILogger<CreateCompanyCommandHandler> logger,
                                               IValidator<CreateCompanyCommad> validator) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public override async Task<ResultSet> HandleCommand(CreateCompanyCommad command, CancellationToken cancellationToken)
        {
            var validationErrors = validator.Validate(command);

            if (validationErrors.Errors.Any())
            {
                return ResultSet.Error($"Error trying create a company. Errors:{validationErrors.Errors.ToString()}");
            }

            var company = new Company
            {
                Name = command.Name
            };

            documentSession.Store(company);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success(company);
        }
    }
}
