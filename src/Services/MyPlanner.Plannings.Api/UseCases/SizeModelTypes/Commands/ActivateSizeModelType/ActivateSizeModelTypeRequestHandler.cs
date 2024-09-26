using MyPlanner.Plannings.Domain.SizeModelTypes;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelType
{
    public class ActivateSizeModelTypeRequestHandler : IRequestHandler<ActivateSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ActivateSizeModelTypeRequestHandler> logger;

        public ActivateSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                   ILogger<ActivateSizeModelTypeRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ActivateSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            entity.Activate();

            if (!entity.IsValid())
            {
                logger.LogError($"SizeModelType code change has errors:{entity.GetBrokenRules()}");
                return false;
            }

            sizeModelTypeRepository.Activate(request.SizeModelTypeId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return true;
        }
    }
}
