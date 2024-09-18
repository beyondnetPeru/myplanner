using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.AddSizeModelTypeFactor
{
    public class AddSizeModelTypeFactorRequestHandler : IRequestHandler<AddSizeModelTypeFactorRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<AddSizeModelTypeFactorRequestHandler> logger;

        public AddSizeModelTypeFactorRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                    ILogger<AddSizeModelTypeFactorRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(AddSizeModelTypeFactorRequest request, CancellationToken cancellationToken)
        {

            var sizeModelType = await sizeModelTypeRepository.GetById(request.SizeModelId);

            var factor = SizeModelTypeFactor.Create(IdValueObject.Create(),
                                                    SizeModelTypeFactorCode.Create(request.Code),
                                                    Name.Create(request.Name), sizeModelType);

            if (!factor.IsValid())
            {
                logger.LogError($"Invalid factor. Error: {factor.GetBrokenRules()}");
                return false;
            }

            sizeModelType.AddFactor(factor);

            if (!sizeModelType.IsValid())
            {
                logger.LogError($"Invalid factor. Error: {factor.GetBrokenRules()}");
                return false;
            }

            sizeModelTypeRepository.AddFactor(factor);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(factor, cancellationToken);

            return true;
        }
    }
}
