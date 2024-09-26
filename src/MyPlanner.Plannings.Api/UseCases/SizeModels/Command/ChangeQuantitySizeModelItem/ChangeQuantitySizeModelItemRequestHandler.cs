using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeQuantitySizeModelItem
{
    public class ChangeQuantitySizeModelItemRequestHandler : IRequestHandler<ChangeQuantitySizeModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<ChangeQuantitySizeModelItemRequestHandler> logger;
        private readonly ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostFactory;

        public ChangeQuantitySizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository,
                                                         ILogger<ChangeQuantitySizeModelItemRequestHandler> logger,
                                                         ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostFactory)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.sizeModelTypeFactorCostFactory = sizeModelTypeFactorCostFactory ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorCostFactory));
        }

        public async Task<bool> Handle(ChangeQuantitySizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            var sizeModel = await sizeModelRepository.Get(sizeModelItem.GetPropsCopy().SizeModelId.GetValue());

            var totalCost = TotalCostCalculator.SetTotalCost(sizeModelTypeFactorCostFactory, sizeModelItem, sizeModel);

            sizeModelItem.ChangeQuantity(request.Quantity, totalCost, UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"Invalid size model item. Errors: {sizeModelItem.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.ChangeQuantity(request.SizeModelItemId, request.Quantity, totalCost);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return true;
        }
    }
}
