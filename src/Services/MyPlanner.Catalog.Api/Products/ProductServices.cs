using MediatR;

namespace MyPlanner.Catalog.Api.UseCases
{
    public class ProductServices
    {
        public ProductServices(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }
    }
}
