using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType
{
    public class CreateSizeModelTypeRequestHandler : IRequestHandler<CreateSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<CreateSizeModelTypeRequestHandler> logger;
        private readonly IMapper mapper;

        public CreateSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<CreateSizeModelTypeRequestHandler> logger, IMapper mapper)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(CreateSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var props = mapper.Map<SizeModelTypeProps>(request);

            var sizeModelType = SizeModelType.Create(IdValueObject.Create(), props.Code, props.Name);

            if (!sizeModelType.IsValid())
            {
                logger.LogError($"SizeModelType is not valid. Errors: {sizeModelType.GetBrokenRules().ToString()}");
                return false;
            }

            await sizeModelTypeRepository.Add(sizeModelType);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelType, cancellationToken);

            return true;

        }
    }
}
