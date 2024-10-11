﻿using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeChangeSizeModelTypeItem
{
    public class ChangeChangeSizeModelTypeItemCommand : ICommand<ResultSet>
    {
        public string SizeModelTypeItemId { get; set; }
        public string PlanItemId { get; set; }
        public string UserId { get; set; }

        public ChangeChangeSizeModelTypeItemCommand(string sizeModelTypeItemId, string planItemId, string userId)
        {
            SizeModelTypeItemId = sizeModelTypeItemId;
            PlanItemId = planItemId;
            UserId = userId;
        }
    }
}
