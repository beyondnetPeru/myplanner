﻿using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModelItem
{
    public class DeactivateSizeModelItemRequest :   ICommand<ResultSet>
    {
        public DeactivateSizeModelItemRequest(string sizeModelId, string sizeModelItemId, string userId)
        {
            SizeModelId = sizeModelId;
            SizeModelItemId = sizeModelItemId;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string SizeModelItemId { get; }
        public string UserId { get; }
    }
}
