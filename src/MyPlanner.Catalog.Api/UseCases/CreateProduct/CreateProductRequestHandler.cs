using MediatR;

namespace MyPlanner.Catalog.Api.UseCases.CreateProduct
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, bool>
    {
        public async Task<bool> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
