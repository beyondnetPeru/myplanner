﻿using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class CreatePlanItemDto : AbstractUserDto
    {
        public CreatePlanItemDto(string userId) : base(userId)
        {
        }

        public string ProductId { get; set; } = default!;
        public string PlanCategoryId { get; set; } = default!;
        public string BusinessFeatureName { get; private set; } = default!;
        public string BusinessFeatureDefinition { get; private set; } = default!;
        public string BusinessFeatureComplexityLevel { get; private set; } = default!;
        public int BusinessFeaturePriority { get; private set; } = default!;
        public string BusinessFeatureMoScoW { get; private set; } = default!;
        public string TechnicalDefinition { get; set; } = default!;
        public string ComponentsImpacted { get; set; } = default!;
        public string TechnicalDependencies { get; set; } = default!;
        public string SizeModelTypeItemId { get; set; } = default!;
        public int BallParkCostSymbol { get; set; } = 1;
        public double BallParkCostAmount { get; set; } = 0.00;
        public double BallparkDependenciesCostAmount { get; set; } = 0.00;
        public double BallParkTotalCostAmount { get; set; } = 0.00;
        public string KeyAssumptions { get; set; } = default!;
    }
}