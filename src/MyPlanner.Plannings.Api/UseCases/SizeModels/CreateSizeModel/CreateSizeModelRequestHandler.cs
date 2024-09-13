using BeyondNet.Ddd.ValueObjects;
using MediatR;
using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.CreateSizeModel
{
    public class CreateSizeModelRequestHandler : IRequestHandler<CreateSizeModelRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ISizeModelTypeFactorRepository sizeModelTypeFactorRepository;
        private readonly ILogger<CreateSizeModelRequestHandler> logger;

        public CreateSizeModelRequestHandler(ISizeModelRepository sizeModelRepository,
                                             ISizeModelTypeRepository sizeModelTypeRepository,
                                             ISizeModelTypeFactorRepository sizeModelTypeFactorRepository,
                                             ILogger<CreateSizeModelRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.sizeModelTypeFactorRepository = sizeModelTypeFactorRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateSizeModelRequest request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeRepository.GetByCode(request.SizeModelTypeCode);

            var sizeModel = SizeModel.Create(IdValueObject.Create(), sizeModelType, Name.Create(request.Name), UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
                logger.LogInformation($"SizeModel is not valid: {sizeModel.GetBrokenRules()}");
                return false;
            }

            if (request.SizeModelItems.Any())
            {
                request.SizeModelItems.Select(async item =>
                {
                    var sizeModelTypeFactor = await sizeModelTypeFactorRepository.GetFactorByCode(item.SizeModelTypeFactorCode);

                    var sizeModelItem = SizeModelItem.Create(IdValueObject.Create(),
                                        sizeModel,
                                        SizeModelProfile.Create(ProfileName.Create(item.ProfileName), ProfileAvgRate.Create(item.ProfileAvgRateAmount)),
                                        sizeModelTypeFactor, SizeModelTypeValueSelected.Create(item.ProfileValueSelected), SizeModelTypeQuantity.Create(item.ProfileQuantity),
                                        SizeModelTotalCost.Create(item.TotalCost), UserId.Create(request.UserId));

                    if (!sizeModelItem.IsValid())
                    {
                        logger.LogInformation($"SizeModelItem is not valid: {sizeModelItem.GetBrokenRules()}, ObjectID:{sizeModelItem.GetPropsCopy().Id}");
                        return;
                    }

                    sizeModel.AddItem(sizeModelItem, UserId.Create(request.UserId));
                });
            }

            await sizeModelRepository.Add(sizeModel);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;

        }
    }
}
