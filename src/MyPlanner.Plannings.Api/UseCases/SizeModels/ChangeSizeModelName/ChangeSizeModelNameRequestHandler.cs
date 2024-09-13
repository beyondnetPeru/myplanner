using MediatR;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.ChangeSizeModelName
{
    public class ChangeSizeModelNameRequestHandler : IRequestHandler<ChangeSizeModelNameRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<ChangeSizeModelNameRequestHandler> logger;

        public ChangeSizeModelNameRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ChangeSizeModelNameRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeSizeModelNameRequest request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            sizeModel.ChangeName(Name.Create(request.Name), UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
                logger.LogError($"SizeModel name cannot be changed. Errors:{sizeModel.GetBrokenRules()}");
                return false;
            }

            await sizeModelRepository.ChangeName(request.SizeModelId, request.Name);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;
        }
    }
}
