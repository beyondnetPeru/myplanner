using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeFactorSizeModel
{
    public class ChangeFactorSizeModelItemCommandHandler : AbstractCommandHandler<ChangeFactorSizeModelItemCommand, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly IFactorCostCalculatorFactory sizeModelTypeFactorCostFactory;

        public ChangeFactorSizeModelItemCommandHandler(ISizeModelRepository sizeModelRepository,
                                                       ILogger<ChangeFactorSizeModelItemCommandHandler> logger,
                                                       IFactorCostCalculatorFactory sizeModelTypeFactorCostFactory):base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeFactorCostFactory = sizeModelTypeFactorCostFactory ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorCostFactory));
        }

        public override async Task<ResultSet> HandleCommand(ChangeFactorSizeModelItemCommand request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            var sizeModel = await sizeModelRepository.Get(sizeModelItem.GetPropsCopy().SizeModelId.GetValue());

            var totalCost = FactorCostCalculatorTotal.SetTotalCost(sizeModelTypeFactorCostFactory, sizeModelItem, sizeModel);

            sizeModelItem.ChangeFactorSelected(Enumeration.FromValue<FactorsEnum>(request.FactorSelected), UserId.Create(request.UserId), totalCost);

            if (!sizeModelItem.IsValid())
            {
                return ResultSet.Error($"SizeModelItem is not valid: {sizeModelItem.GetBrokenRules()}");                
            }

            sizeModelRepository.UpdateItem(sizeModelItem);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
