using BeyondNet.Cqrs.Impl;
using BeyondNet.Cqrs.Interfaces;
using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductCommand(string companyId, string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<ResultSet>;

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.companyId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
    public class UpdateProductCommandHandler : AbstractCommandHandler<UpdateProductCommand, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly ILogger<UpdateProductCommandHandler> logger;

        public UpdateProductCommandHandler(IDocumentSession documentSession, ILogger<UpdateProductCommandHandler> logger) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ResultSet> HandleCommand(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);

            if (product == null)
            {
                return ResultSet.Error($"Product {request.Id} was not found");
            }

            product.CompanyId = request.companyId;
            product.Name = request.Name;
            product.Category = request.Category;
            product.Description = request.Description;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;

            documentSession.Update(product);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success("Product updated sucessfully");
        }
    }
}
