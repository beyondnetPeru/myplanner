using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModel
{
    public class ActivateSizeModelCommandtHandler : AbstractCommandHandler<ActivateSizeModelCommand, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;

        public ActivateSizeModelCommandtHandler(ISizeModelRepository sizeModelRepository, ILogger<ActivateSizeModelCommandtHandler> logger):base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
        }

        public override async Task<ResultSet> HandleCommand(ActivateSizeModelCommand request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            sizeModel.Activate(UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
                return ResultSet.Error($"SizeModel with id {request.SizeModelId} is not valid. Errors: {sizeModel.GetBrokenRules()}");
            }

            sizeModelRepository.Update(sizeModel);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModel, cancellationToken);

            return ResultSet.Success("SizeModel activated successfully");

        }
    }
}
