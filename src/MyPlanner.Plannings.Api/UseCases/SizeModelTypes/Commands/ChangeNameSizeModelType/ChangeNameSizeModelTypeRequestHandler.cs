using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeRequestHandler : IRequestHandler<ChangeNameSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ChangeNameSizeModelTypeRequestHandler> logger;

        public ChangeNameSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                     ILogger<ChangeNameSizeModelTypeRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeNameSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            entity.ChangeName(Name.Create(request.Name));

            if (!entity.IsValid())
            {
                logger.LogError($"SizeModelType code change has errors:{entity.GetBrokenRules()}");
                return false;
            }

            sizeModelTypeRepository.ChangeName(request.SizeModelTypeId, entity.GetPropsCopy().Name.GetValue());

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return true;
        }
    }
}
