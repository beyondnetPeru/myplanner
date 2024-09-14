using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelType
{
    public class ChangeCodeSizeModelTypeRequestHandler : IRequestHandler<ChangeCodeSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ChangeCodeSizeModelTypeRequestHandler> logger;

        public ChangeCodeSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<ChangeCodeSizeModelTypeRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> Handle(ChangeCodeSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);


            entity.ChangeCode(SizeModelTypeCode.Create(request.Code));

            if (!entity.IsValid())
            {
                logger.LogError($"SizeModelType code change has errors:{entity.GetBrokenRules()}");
                return false;
            }

            await sizeModelTypeRepository.ChangeCode(request.SizeModelTypeId, entity.GetPropsCopy().Code.GetValue());

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return true;

        }
    }
}
