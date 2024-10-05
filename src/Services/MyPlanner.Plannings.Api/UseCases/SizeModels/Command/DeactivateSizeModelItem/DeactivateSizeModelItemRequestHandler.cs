﻿using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModelItem
{
    public class DeactivateSizeModelItemRequestHandler : AbstractCommandHandler<DeactivateSizeModelItemRequest, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;

        public DeactivateSizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<DeactivateSizeModelItemRequestHandler> logger) : base(logger)
        {
            this.sizeModelRepository = sizeModelRepository;
        }

        public override async Task<ResultSet> HandleCommand(DeactivateSizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            sizeModelItem.Deactivate(UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                return ResultSet.Error($"SizeModelItem is not valid. Errors:{sizeModelItem.GetBrokenRules()}");
            }

            sizeModelRepository.DeactiveItem(request.SizeModelItemId);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return ResultSet.Success();
        }
    }
}
