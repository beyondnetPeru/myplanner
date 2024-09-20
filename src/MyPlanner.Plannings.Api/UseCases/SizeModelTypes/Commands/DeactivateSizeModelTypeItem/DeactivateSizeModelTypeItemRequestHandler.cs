using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeItemRequestHandler : IRequestHandler<DeactivateSizeModelTypeItemRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<DeactivateSizeModelTypeItemRequestHandler> logger;

        public DeactivateSizeModelTypeItemRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<DeactivateSizeModelTypeItemRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeactivateSizeModelTypeItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            sizeModelTypeItem.Deactivate();

            if (!sizeModelTypeItem.IsValid())
            {
                logger.LogError($"Error deactivating size model type factor {request.SizeModelTypeItemId}. Errors:{sizeModelTypeItem.GetBrokenRules()}");
                return false;
            }

            sizeModelTypeRepository.Deactivate(request.SizeModelTypeItemId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelTypeItem, cancellationToken);

            return true;


        }
    }
}
