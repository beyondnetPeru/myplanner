using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModel
{
    public class CreateSizeModelCommandHandler : AbstractCommandHandler<CreateSizeModelCommand, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostCalculatorFactory;

        public CreateSizeModelCommandHandler(ISizeModelRepository sizeModelRepository,
                                             ISizeModelTypeRepository sizeModelTypeRepository,
                                             ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostCalculatorFactory,
                                             ILogger<CreateSizeModelCommandHandler> logger) : base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.sizeModelTypeFactorCostCalculatorFactory = sizeModelTypeFactorCostCalculatorFactory ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorCostCalculatorFactory));
        }

        public override async Task<ResultSet> HandleCommand(CreateSizeModelCommand request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            var sizeModel = SizeModel.Create(IdValueObject.Create(), sizeModelType.GetPropsCopy().Id, sizeModelType.GetPropsCopy().Code, Name.Create(request.Name), UserId.Create(request.UserId));


            if (!sizeModel.IsValid())
            {
                return ResultSet.Error($"SizeModel is not valid: {sizeModel.GetBrokenRules()}");
            }

            if (request.Items.Any())
            {
                foreach (var item in request.Items)
                {
                    var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(item.SizeModelTypeItemId);

                    SetTotalCost(request, item);

                    var sizeModelItem = SizeModelItem.Create(IdValueObject.Create(),
                                                             sizeModel.GetPropsCopy().Id,
                                                             sizeModelTypeItem.GetPropsCopy().Id,
                                                             sizeModelTypeItem.GetPropsCopy().Code,
                                                             Enumeration.FromValue<FactorsEnum>(item.FactorSelected),
                                                             SizeModelProfile.Create(ProfileName.Create(item.ProfileName),
                                                                                     ProfileAvgRate.Create(Enumeration.FromValue<CurrencySymbolEnum>(item.ProfileAvgRateSymbol), item.ProfileAvgRateValue)),
                                                             SizeModelTypeQuantity.Create(item.Quantity),
                                                             SizeModelTotalCost.Create(item.TotalCost),
                                                             SizeModelItemIsStandard.Create(item.IsStandard),
                                                             UserId.Create(item.UserId));

                    if (!sizeModelItem.IsValid())
                    {
                        return ResultSet.Error($"SizeModelItem is not valid: {sizeModelItem.GetBrokenRules()}");
                    }

                    sizeModel.AddItem(sizeModelItem, UserId.Create(request.UserId));
                }

            }

            sizeModelRepository.Add(sizeModel);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModel, cancellationToken);

            return ResultSet.Success();
        }

        private void SetTotalCost(CreateSizeModelCommand request, CreateSizeModelItemRequest item)
        {
            var costCalculator = this.sizeModelTypeFactorCostCalculatorFactory.Create(Enumeration.FromValue<FactorsEnum>(item.FactorSelected),
                                                                                      request.SizeModelTypeCode);

            item.TotalCost = costCalculator.Calculate(Enumeration.FromValue<FactorsEnum>(item.FactorSelected),
                                                                                         item.SizeModelTypeItemCode,
                                                                                         item.Quantity,
                                                                                         item.ProfileAvgRateValue);
        }
    }
}
