using MediatR;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.DeactivateSizeModel
{
    public class DeactivateSizeModelRequestHandler : IRequestHandler<DeactivateSizeModelRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<DeactivateSizeModelRequest> logger;

        public DeactivateSizeModelRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<DeactivateSizeModelRequest> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeactivateSizeModelRequest request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            sizeModel.Deactivate(UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
                logger.LogError($"SizeModel is not valid. Errors:{sizeModel.GetBrokenRules()}");
                return false;
            }

            await sizeModelRepository.Deactivate(sizeModel.GetPropsCopy().Id.GetValue());

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;
        }
    }
}
