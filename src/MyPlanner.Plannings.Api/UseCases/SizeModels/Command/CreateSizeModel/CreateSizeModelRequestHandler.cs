using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModel
{
    public class CreateSizeModelRequestHandler : IRequestHandler<CreateSizeModelRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<CreateSizeModelRequestHandler> logger;

        public CreateSizeModelRequestHandler(ISizeModelRepository sizeModelRepository,
                                             ISizeModelTypeRepository sizeModelTypeRepository,
                                             ILogger<CreateSizeModelRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateSizeModelRequest request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);
            var sizeModel = SizeModel.Create(IdValueObject.Create(), sizeModelType, Name.Create(request.Name), UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
                logger.LogInformation($"SizeModel is not valid: {sizeModel.GetBrokenRules()}");
                return false;
            }

            await sizeModelRepository.Add(sizeModel);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModel, cancellationToken);

            return true;

        }
    }
}
