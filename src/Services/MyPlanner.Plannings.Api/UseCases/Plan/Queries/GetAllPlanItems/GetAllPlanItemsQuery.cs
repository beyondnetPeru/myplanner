﻿using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlanItems
{
    public class GetAllPlanItemsQuery : IQuery<ResultSet>
    {
        public string PlanId { get; set; }

        public GetAllPlanItemsQuery(string planId)
        {
            PlanId = planId;
        }
    }
}
