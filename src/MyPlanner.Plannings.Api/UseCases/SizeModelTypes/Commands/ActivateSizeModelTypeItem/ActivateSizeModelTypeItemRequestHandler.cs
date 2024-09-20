using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeItemRequestHandler : IRequestHandler<ActivateSizeModelTypeItemRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ActivateSizeModelTypeItemRequestHandler> logger;

        public ActivateSizeModelTypeItemRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<ActivateSizeModelTypeItemRequestHandler> logger)
        {

            this.sizeModelTypeRepository = sizeModelTypeRepository;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ActivateSizeModelTypeItemRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            entity.Activate();

            if (!entity.IsValid())
            {
                logger.LogError("Error activating SizeModelTypeItem: {0}", entity.GetBrokenRules());
                return false;
            }

            sizeModelTypeRepository.Activate(request.SizeModelTypeItemId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return true;
        }
    }
}
