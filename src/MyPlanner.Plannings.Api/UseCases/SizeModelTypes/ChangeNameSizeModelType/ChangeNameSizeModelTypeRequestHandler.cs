using MediatR;
using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeRequestHandler : IRequestHandler<ChangeNameSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ChangeNameSizeModelTypeRequestHandler> logger;

        public ChangeNameSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<ChangeNameSizeModelTypeRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeNameSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.Id);

            entity.ChangeName(Name.Create(request.Name));

            if (!entity.IsValid())
            {
                logger.LogError($"SizeModelType code change has errors:{entity.GetBrokenRules()}");
                return false;
            }

            await sizeModelTypeRepository.ChangeName(request.Id, entity.GetPropsCopy().Name.GetValue());

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return true;
        }
    }
}
