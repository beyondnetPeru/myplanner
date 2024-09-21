using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelTypeItem
{
    public class ChangeSizeModelTypeItemModelItemRequestHandler : IRequestHandler<ChangeSizeModelTypeItemModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ChangeSizeModelTypeItemModelItemRequestHandler> logger;

        public ChangeSizeModelTypeItemModelItemRequestHandler(ISizeModelRepository sizeModelRepository,
                                                     ISizeModelTypeRepository sizeModelTypeRepository,
                                                     ILogger<ChangeSizeModelTypeItemModelItemRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeSizeModelTypeItemModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelItemTypeId);

            sizeModelItem.ChangeSizeModelTypeItem(sizeModelTypeItem, UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"SizeModelItem is not valid. Errors: {sizeModelItem.GetBrokenRules().ToString()}");
                return false;
            }

            sizeModelRepository.ChangeSizeModelTypeItem(request.SizeModelItemId, request.SizeModelItemTypeId);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return true;

        }
    }
}
