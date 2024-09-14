using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeFactorRequestHandler : IRequestHandler<DeactivateSizeModelTypeFactorRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<DeactivateSizeModelTypeFactorRequestHandler> logger;

        public DeactivateSizeModelTypeFactorRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<DeactivateSizeModelTypeFactorRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeactivateSizeModelTypeFactorRequest request, CancellationToken cancellationToken)
        {
            var sizeModelTypeFactor = await sizeModelTypeRepository.GetFactorById(request.SizeModelTypeFactorId);

            sizeModelTypeFactor.Deactivate();

            if (!sizeModelTypeFactor.IsValid())
            {
                logger.LogError($"Error deactivating size model type factor {request.SizeModelTypeFactorId}. Errors:{sizeModelTypeFactor.GetBrokenRules()}");
                return false;
            }

            await sizeModelTypeRepository.Deactivate(request.SizeModelTypeFactorId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelTypeFactor, cancellationToken);

            return true;


        }
    }
}
