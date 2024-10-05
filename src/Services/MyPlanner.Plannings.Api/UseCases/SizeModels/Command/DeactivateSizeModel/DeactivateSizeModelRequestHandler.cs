using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModel
{
    public class DeactivateSizeModelRequestHandler : AbstractCommandHandler<DeactivateSizeModelRequest, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;

        public DeactivateSizeModelRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<DeactivateSizeModelRequestHandler> logger) : base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
        }

        public override async Task<ResultSet> HandleCommand(DeactivateSizeModelRequest request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            sizeModel.Deactivate(UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
              return ResultSet.Error($"SizeModel is not valid. Errors:{sizeModel.GetBrokenRules()}");
            }

            sizeModelRepository.Deactivate(sizeModel.GetPropsCopy().Id.GetValue());

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return ResultSet.Success();
        }
    }
}
