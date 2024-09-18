using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModelItem
{
    public class CreateSizeModelItemRequestHandler : IRequestHandler<CreateSizeModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<CreateSizeModelItemRequestHandler> logger;

        public CreateSizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository, ISizeModelTypeRepository sizeModelTypeRepository, ILogger<CreateSizeModelItemRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateSizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelTypeFactor = await sizeModelTypeRepository.GetFactorById(request.SizeModelTypeFactorId);

            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            var sizeModelItem = SizeModelItem.Create(IdValueObject.Create(),
                sizeModel,
                SizeModelProfile.Create(ProfileName.Create(request.ProfileName), ProfileAvgRate.Create(request.ProfileAvgRateAmount)),
                sizeModelTypeFactor,
                SizeModelTypeValueSelected.Create(request.ProfileValueSelected),
                SizeModelTypeQuantity.Create(request.ProfileQuantity),
                SizeModelTotalCost.Create(request.TotalCost),
                UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"SizeModelItem is not valid. Errors:{sizeModelItem.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.AddItem(sizeModelItem);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;

        }
    }
}
