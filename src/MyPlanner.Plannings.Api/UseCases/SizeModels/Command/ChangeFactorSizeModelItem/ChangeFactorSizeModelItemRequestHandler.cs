using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeFactorSizeModel
{
    public class ChangeFactorSizeModelItemRequestHandler : IRequestHandler<ChangeFactorSizeModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<ChangeFactorSizeModelItemRequestHandler> logger;
        private readonly ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostFactory;

        public ChangeFactorSizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository,
                                                       ILogger<ChangeFactorSizeModelItemRequestHandler> logger,
                                                       ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostFactory)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.sizeModelTypeFactorCostFactory = sizeModelTypeFactorCostFactory ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorCostFactory));
        }

        public async Task<bool> Handle(ChangeFactorSizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            var sizeModel = await sizeModelRepository.Get(sizeModelItem.GetPropsCopy().SizeModelId.GetValue());

            var totalCost = TotalCostCalculator.SetTotalCost(sizeModelTypeFactorCostFactory, sizeModelItem, sizeModel);

            sizeModelItem.ChangeFactorSelected(Enumeration.FromValue<FactorsEnum>(request.FactorSelected), UserId.Create(request.UserId), totalCost);

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"SizeModelItem is not valid: {sizeModelItem.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.ChangeFactorSelected(request.SizeModelItemId, request.FactorSelected, totalCost);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return true;
        }
    }
}
