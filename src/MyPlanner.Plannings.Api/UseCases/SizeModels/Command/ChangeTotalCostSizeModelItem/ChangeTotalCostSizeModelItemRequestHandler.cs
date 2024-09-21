using MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeQuantitySizeModelItem;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeTotalCostSizeModelItem
{
    public class ChangeTotalCostSizeModelItemRequestHandler : IRequestHandler<ChangeTotalCostSizeModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<ChangeQuantitySizeModelItemRequestHandler> logger;

        public ChangeTotalCostSizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ChangeQuantitySizeModelItemRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeTotalCostSizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            sizeModelItem.ChangeTotalCost(request.TotalCost, UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"Invalid size model item. Errors: {sizeModelItem.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.ChangeTotalCost(request.SizeModelItemId, request.TotalCost);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return true;
        }
    }
}
