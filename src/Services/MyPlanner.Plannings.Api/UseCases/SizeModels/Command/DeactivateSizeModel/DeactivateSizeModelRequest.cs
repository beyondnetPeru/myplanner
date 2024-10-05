﻿using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModel
{
    public class DeactivateSizeModelRequest : ICommand<ResultSet>
    {
        public DeactivateSizeModelRequest(string sizeModelId, string userId)
        {
            SizeModelId = sizeModelId;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string UserId { get; }
    }
}
