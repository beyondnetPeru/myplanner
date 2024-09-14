using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.CreateSizeModelType
{
    public class CreateSizeModelTypeRequestHandler : IRequestHandler<CreateSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<CreateSizeModelTypeRequestHandler> logger;

        public CreateSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<CreateSizeModelTypeRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var sizeModelType = SizeModelType.Create(
                IdValueObject.Create(),
                SizeModelTypeCode.Create(request.Code),
                Name.Create(request.Name));

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
