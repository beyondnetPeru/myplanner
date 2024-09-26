using MediatR;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModel
{
    public class ActivateSizeModelRequestHandler : IRequestHandler<ActivateSizeModelRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<ActivateSizeModelRequestHandler> logger;

        public ActivateSizeModelRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ActivateSizeModelRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> Handle(ActivateSizeModelRequest request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            sizeModel.Activate(UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
                logger.LogError($"SizeModel with id {request.SizeModelId} is not valid. Errors: {sizeModel.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.Activate(sizeModel.GetPropsCopy().Id.GetValue());

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModel, cancellationToken);

            return true;

        }
    }
}
