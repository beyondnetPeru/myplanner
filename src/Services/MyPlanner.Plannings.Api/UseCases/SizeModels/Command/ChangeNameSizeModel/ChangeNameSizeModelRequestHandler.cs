using MediatR;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelName
{
    public class ChangeNameSizeModelRequestHandler : IRequestHandler<ChangeNameSizeModelRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<ChangeNameSizeModelRequestHandler> logger;

        public ChangeNameSizeModelRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ChangeNameSizeModelRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeNameSizeModelRequest request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            sizeModel.ChangeName(Name.Create(request.Name), UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
                logger.LogError($"SizeModel name cannot be changed. Errors:{sizeModel.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.ChangeName(request.SizeModelId, request.Name);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModel, cancellationToken);

            return true;
        }
    }
}
