using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelType
{
    public class DeleteSizeModelTypeCommandHandler : AbstractCommandHandler<DeleteSizeModelTypeCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public DeleteSizeModelTypeCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<DeleteSizeModelTypeCommandHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleCommand(DeleteSizeModelTypeCommand request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            sizeModelType.Delete();

            if (!sizeModelType.IsValid())
            {
                return ResultSet.Error($"SizeModelType cannot be deleted. Errors: {sizeModelType.GetBrokenRules().ToString()}");
            }

            sizeModelTypeRepository.Delete(request.SizeModelTypeId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelType, cancellationToken);

            return ResultSet.Success();
        }
    }
}
