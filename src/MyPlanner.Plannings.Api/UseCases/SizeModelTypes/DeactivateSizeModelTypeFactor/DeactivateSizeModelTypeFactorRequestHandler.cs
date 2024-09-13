using MediatR;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeFactorRequestHandler : IRequestHandler<DeactivateSizeModelTypeFactorRequest, bool>
    {
        private readonly ISizeModelTypeFactorRepository sizeModelTypeFactorRepository;
        private readonly ILogger<DeactivateSizeModelTypeFactorRequestHandler> logger;

        public DeactivateSizeModelTypeFactorRequestHandler(ISizeModelTypeFactorRepository sizeModelTypeFactorRepository, ILogger<DeactivateSizeModelTypeFactorRequestHandler> logger)
        {
            this.sizeModelTypeFactorRepository = sizeModelTypeFactorRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeactivateSizeModelTypeFactorRequest request, CancellationToken cancellationToken)
        {
            var sizeModelTypeFactor = await sizeModelTypeFactorRepository.GetFactorById(request.SizeModelTypeFactorId);

            sizeModelTypeFactor.Deactivate();

            if (!sizeModelTypeFactor.IsValid())
            {
                logger.LogError($"Error deactivating size model type factor {request.SizeModelTypeFactorId}. Errors:{sizeModelTypeFactor.GetBrokenRules()}");
                return false;
            }

            await sizeModelTypeFactorRepository.Deactivate(sizeModelTypeFactor);

            await sizeModelTypeFactorRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;


        }
    }
}
