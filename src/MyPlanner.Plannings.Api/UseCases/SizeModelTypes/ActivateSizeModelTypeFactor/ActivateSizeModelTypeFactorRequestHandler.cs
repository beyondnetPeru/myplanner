using MediatR;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeFactorRequestHandler : IRequestHandler<ActivateSizeModelTypeFactorRequest, bool>
    {
        private readonly ISizeModelTypeFactorRepository sizeModelTypeFactorRepository;
        private readonly ILogger<ActivateSizeModelTypeFactorRequestHandler> logger;

        public ActivateSizeModelTypeFactorRequestHandler(ISizeModelTypeFactorRepository sizeModelTypeFactorRepository, ILogger<ActivateSizeModelTypeFactorRequestHandler> logger)
        {
            this.sizeModelTypeFactorRepository = sizeModelTypeFactorRepository;
            this.logger = logger;
        }

        public async Task<bool> Handle(ActivateSizeModelTypeFactorRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeFactorRepository.GetFactorById(request.SizeModelTypeFactorId);

            entity.Activate();

            if (!entity.IsValid())
            {
                logger.LogError("Error activating SizeModelTypeFactor: {0}", entity.GetBrokenRules());
                return false;
            }

            await sizeModelTypeFactorRepository.Activate(entity);

            await sizeModelTypeFactorRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;
        }
    }
}
