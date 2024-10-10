using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeQuantitySizeModelItem
{
    public class ChangeQuantitySizeModelItemCommandHandler : AbstractCommandHandler<ChangeQuantitySizeModelItemCommand, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly IFactorCostCalculatorFactory sizeModelTypeFactorCostFactory;

        public ChangeQuantitySizeModelItemCommandHandler(ISizeModelRepository sizeModelRepository,
                                                         ILogger<ChangeQuantitySizeModelItemCommandHandler> logger,
                                                         IFactorCostCalculatorFactory sizeModelTypeFactorCostFactory):base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeFactorCostFactory = sizeModelTypeFactorCostFactory ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorCostFactory));
        }

        public override async Task<ResultSet> HandleCommand(ChangeQuantitySizeModelItemCommand request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            var sizeModel = await sizeModelRepository.Get(sizeModelItem.GetPropsCopy().SizeModelId.GetValue());

            var totalCost = FactorCostCalculatorTotal.SetTotalCost(sizeModelTypeFactorCostFactory, sizeModelItem, sizeModel);

            sizeModelItem.ChangeQuantity(request.Quantity, totalCost, UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                return ResultSet.Error($"Invalid size model item. Errors: {sizeModelItem.GetBrokenRules().ToString()}");                
            }

            sizeModelRepository.UpdateItem(sizeModelItem);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
