using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModel
{
    public class CreateSizeModelRequestHandler : IRequestHandler<CreateSizeModelRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<CreateSizeModelRequestHandler> logger;
        private readonly ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostCalculatorFactory;

        public CreateSizeModelRequestHandler(ISizeModelRepository sizeModelRepository,
                                             ISizeModelTypeRepository sizeModelTypeRepository,
                                             ILogger<CreateSizeModelRequestHandler> logger,
                                             ISizeModelTypeFactorCostFactory sizeModelTypeFactorCostCalculatorFactory)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.sizeModelTypeFactorCostCalculatorFactory = sizeModelTypeFactorCostCalculatorFactory ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorCostCalculatorFactory));
        }

        public async Task<bool> Handle(CreateSizeModelRequest request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            var sizeModel = SizeModel.Create(IdValueObject.Create(), sizeModelType, Name.Create(request.Name), UserId.Create(request.UserId));


            if (!sizeModel.IsValid())
            {
                logger.LogInformation($"SizeModel is not valid: {sizeModel.GetBrokenRules()}");
                return false;
            }

            if (request.Items.Any())
            {
                foreach (var item in request.Items)
                {
                    var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(item.SizeModelTypeItemId);

                    var costCalculator = this.sizeModelTypeFactorCostCalculatorFactory.Create(Enumeration.FromValue<FactorsEnum>(item.FactorSelected),
                                    sizeModelTypeItem.GetPropsCopy().Code.GetValue().ToLower().ToString());

                    item.TotalCost = costCalculator.Calculate(Enumeration.FromValue<FactorsEnum>(item.FactorSelected),
                                             item.SizeModelTypeSelected,
                                             item.ProfileAvgRateAmount);


                    var sizeModelItem = SizeModelItem.Create(IdValueObject.Create(),
                                                             sizeModel,
                                                             sizeModelTypeItem,
                                                             Enumeration.FromValue<FactorsEnum>(item.FactorSelected),
                                                             SizeModelProfile.Create(ProfileName.Create(item.ProfileName), ProfileAvgRate.Create(item.ProfileAvgRateAmount)),
                                                             SizeModelTypeValueSelected.Create(item.SizeModelTypeSelected),
                                                             SizeModelTypeQuantity.Create(item.Quantity),
                                                             SizeModelTotalCost.Create(item.TotalCost),
                                                             SizeModelItemIsStandard.Create(item.IsStandard),
                                                             UserId.Create(item.UserId));

                    if (!sizeModelItem.IsValid())
                    {
                        logger.LogInformation($"SizeModelItem is not valid: {sizeModelItem.GetBrokenRules()}");
                        return false;
                    }

                    sizeModel.AddItem(sizeModelItem, UserId.Create(request.UserId));
                }

            }

            sizeModelRepository.Add(sizeModel);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModel, cancellationToken);

            return true;
        }
    }
}
