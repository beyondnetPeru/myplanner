﻿using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Domain.SizeModelTypes;

using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelTypeItem
{
    public class ChangeSizeModelTypeItemModelItemCommandHandler : AbstractCommandHandler<ChangeSizeModelTypeItemModelItemCommand, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly IFactorCostCalculatorFactory sizeModelTypeFactorCostFactory;

        public ChangeSizeModelTypeItemModelItemCommandHandler(ISizeModelRepository sizeModelRepository,
                                                     ISizeModelTypeRepository sizeModelTypeRepository,
                                                     ILogger<ChangeSizeModelTypeItemModelItemCommandHandler> logger,
                                                     IFactorCostCalculatorFactory sizeModelTypeFactorCostFactory) : base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.sizeModelTypeFactorCostFactory = sizeModelTypeFactorCostFactory ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorCostFactory));
        }

        public override async Task<ResultSet> HandleCommand(ChangeSizeModelTypeItemModelItemCommand request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            var sizeModel = await sizeModelRepository.Get(sizeModelItem.GetPropsCopy().SizeModelId.GetValue());

            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelItemTypeId);

            sizeModelItem.ChangeSizeModelTypeItem(sizeModelTypeItem.GetPropsCopy().Id,
                                                  sizeModelTypeItem.GetPropsCopy().Code,
                                                  UserId.Create(request.UserId));

            var totalCost = FactorCostCalculatorTotal.SetTotalCost(sizeModelTypeFactorCostFactory, sizeModelItem, sizeModel);

            sizeModelItem.ChangeTotalCost(totalCost, UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                return ResultSet.Error($"SizeModelItem is not valid. Errors: {sizeModelItem.GetBrokenRules().ToString()}");
            }

            sizeModelRepository.UpdateItem(sizeModelItem);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
