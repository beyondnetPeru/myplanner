using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModel
{
    public class DeactivateSizeModelCommandHandler : AbstractCommandHandler<DeactivateSizeModelCommand, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;

        public DeactivateSizeModelCommandHandler(ISizeModelRepository sizeModelRepository, ILogger<DeactivateSizeModelCommandHandler> logger) : base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
        }

        public override async Task<ResultSet> HandleCommand(DeactivateSizeModelCommand request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            sizeModel.Deactivate(UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
              return ResultSet.Error($"SizeModel is not valid. Errors:{sizeModel.GetBrokenRules()}");
            }

            sizeModelRepository.Update(sizeModel);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return ResultSet.Success();
        }
    }
}
