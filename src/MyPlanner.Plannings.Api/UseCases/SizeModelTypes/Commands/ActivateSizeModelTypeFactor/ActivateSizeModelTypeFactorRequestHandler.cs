using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeFactorRequestHandler : IRequestHandler<ActivateSizeModelTypeFactorRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ActivateSizeModelTypeFactorRequestHandler> logger;

        public ActivateSizeModelTypeFactorRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<ActivateSizeModelTypeFactorRequestHandler> logger)
        {

            this.sizeModelTypeRepository = sizeModelTypeRepository;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ActivateSizeModelTypeFactorRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetFactorById(request.SizeModelTypeFactorId);

            entity.Activate();

            if (!entity.IsValid())
            {
                logger.LogError("Error activating SizeModelTypeFactor: {0}", entity.GetBrokenRules());
                return false;
            }

            await sizeModelTypeRepository.Activate(request.SizeModelTypeFactorId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return true;
        }
    }
}
