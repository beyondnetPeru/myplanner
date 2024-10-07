﻿using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelTypeItem
{
    public class ChangeNameSizeModelTypeItemCommand : ICommand<ResultSet>
    {
        public string SizeModelTypeItemId { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }

        public ChangeNameSizeModelTypeItemCommand(string sizeModelTypeItemId, string name, string userId)
        {
            SizeModelTypeItemId = sizeModelTypeItemId;
            Name = name;
            UserId = userId;
        }
    }
}