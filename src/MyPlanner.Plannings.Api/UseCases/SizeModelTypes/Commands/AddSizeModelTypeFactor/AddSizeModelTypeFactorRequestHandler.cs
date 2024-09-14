using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.AddSizeModelTypeFactor
{
    public class AddSizeModelTypeFactorRequestHandler : IRequestHandler<AddSizeModelTypeFactorRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<AddSizeModelTypeFactorRequestHandler> logger;
        private readonly IMapper mapper;

        public AddSizeModelTypeFactorRequestHandler(ISizeModelTypeRepository modelTypeRepository,
                                                    ILogger<AddSizeModelTypeFactorRequestHandler> logger,
                                                    IMapper mapper)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(AddSizeModelTypeFactorRequest request, CancellationToken cancellationToken)
        {
            var dto = await sizeModelTypeRepository.GetById(request.SizeModelId);

            var sizeModelType = mapper.Map<SizeModelType>(dto);

            var factor = SizeModelTypeFactor.Create(IdValueObject.Create(),
                                                    SizeModelTypeFactorCode.Create(request.Code),
                                                    Name.Create(request.Name), sizeModelType);

            sizeModelType.AddFactor(factor);

            if (!factor.IsValid())
            {
                logger.LogError($"Invalid factor. Error: {factor.GetBrokenRules()}");
                return false;
            }

            await sizeModelTypeRepository.AddFactor(factor);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(factor, cancellationToken);

            return true;
        }
    }
}
