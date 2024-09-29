using FluentValidation;
using MyPlanner.Catalog.Api.Models;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Catalog.Api.Products
{
    public record CreateProductCommand : ICommand<CreateProductResult>
    {
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }

    public record CreateProductResult(string Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession documentSession, ILogger<CreateProductCommandHandler> logger, IValidator<CreateProductCommand> validator) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            documentSession.Store(product);

            await documentSession.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
