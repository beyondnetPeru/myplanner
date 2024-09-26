using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelTypeItem
{
    public class ChangeSizeModelTypeItemModelItemRequestHandler : IRequestHandler<ChangeSizeModelTypeItemModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ChangeSizeModelTypeItemModelItemRequestHandler> logger;
        private readonly ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostFactory;

        public ChangeSizeModelTypeItemModelItemRequestHandler(ISizeModelRepository sizeModelRepository,
                                                     ISizeModelTypeRepository sizeModelTypeRepository,
                                                     ILogger<ChangeSizeModelTypeItemModelItemRequestHandler> logger,
                                                     ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostFactory)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.sizeModelTypeFactorCostFactory = sizeModelTypeFactorCostFactory ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorCostFactory));
        }

        public async Task<bool> Handle(ChangeSizeModelTypeItemModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            var sizeModel = await sizeModelRepository.Get(sizeModelItem.GetPropsCopy().SizeModelId.GetValue());

            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelItemTypeId);

            sizeModelItem.ChangeSizeModelTypeItem(sizeModelTypeItem.GetPropsCopy().Id,
                                                  sizeModelTypeItem.GetPropsCopy().Code,
                                                  UserId.Create(request.UserId));

            var totalCost = TotalCostCalculator.SetTotalCost(sizeModelTypeFactorCostFactory, sizeModelItem, sizeModel);

            sizeModelItem.ChangeTotalCost(totalCost, UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"SizeModelItem is not valid. Errors: {sizeModelItem.GetBrokenRules().ToString()}");
                return false;
            }

            sizeModelRepository.ChangeSizeModelTypeItem(request.SizeModelItemId, request.SizeModelItemTypeId, request.SizeModelItemTypeCode, totalCost);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return true;

        }
    }
}
