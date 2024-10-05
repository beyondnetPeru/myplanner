﻿using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModel
{
    public class ActivateSizeModelRequestHandler : AbstractCommandHandler<ActivateSizeModelRequest, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;

        public ActivateSizeModelRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ActivateSizeModelRequestHandler> logger):base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
        }

        public override async Task<ResultSet> HandleCommand(ActivateSizeModelRequest request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            sizeModel.Activate(UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
                return ResultSet.Error($"SizeModel with id {request.SizeModelId} is not valid. Errors: {sizeModel.GetBrokenRules()}");
            }

            sizeModelRepository.Activate(sizeModel.GetPropsCopy().Id.GetValue());

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModel, cancellationToken);

            return ResultSet.Success("SizeModel activated successfully");

        }
    }
}
