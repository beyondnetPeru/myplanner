﻿using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelName
{
    public class ChangeNameSizeModelRequest : ICommand<ResultSet>
    {
        public ChangeNameSizeModelRequest(string sizeModelId, string name, string userId)
        {
            SizeModelId = sizeModelId;
            Name = name;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string Name { get; }
        public string UserId { get; }
    }
}
