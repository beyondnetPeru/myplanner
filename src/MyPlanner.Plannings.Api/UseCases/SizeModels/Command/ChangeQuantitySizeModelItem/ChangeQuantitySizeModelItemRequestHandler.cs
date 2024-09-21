using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeQuantitySizeModelItem
{
    public class ChangeQuantitySizeModelItemRequestHandler : IRequestHandler<ChangeQuantitySizeModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<ChangeQuantitySizeModelItemRequestHandler> logger;

        public ChangeQuantitySizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ChangeQuantitySizeModelItemRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeQuantitySizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            sizeModelItem.ChangeQuantity(request.Quantity, UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"Invalid size model item. Errors: {sizeModelItem.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.ChangeQuantity(request.SizeModelItemId, request.Quantity);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return true;
        }
    }
}
