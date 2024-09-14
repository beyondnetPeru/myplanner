using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelType
{
    public class DeactivateSizeModelTypeRequestHandler : IRequestHandler<DeactivateSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<DeactivateSizeModelTypeRequestHandler> logger;

        public DeactivateSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                     ILogger<DeactivateSizeModelTypeRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeactivateSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            entity.Deactivate();

            if (!entity.IsValid())
            {
                logger.LogError($"SizeModelType code change has errors:{entity.GetBrokenRules()}");
                return false;
            }

            await sizeModelTypeRepository.Deactivate(request.SizeModelTypeId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return true;
        }
    }
}
