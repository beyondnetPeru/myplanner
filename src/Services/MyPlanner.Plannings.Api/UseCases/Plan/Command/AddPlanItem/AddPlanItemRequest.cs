namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.AddPlanItem
{
    public class AddPlanItemRequest : IRequest<bool>
    {
        public string PlanId { get; set; }
        public string CategoryName { get; internal set; }
        public string Name { get; private set; }
        public string BusinessDefinition { get; private set; }
        public string ComplexityLevel { get; private set; }
        public string BacklogName { get; private set; }
        public int Priority { get; private set; }
        public string MoScoW { get; set; }
        public string SizeModelTypeFactorId { get; set; }
        public string SizeModelTypeValueSelected { get; set; }
        public string BusinessFeature { get; set; }
        public string TechnicalDefinition { get; set; }
        public string ComponentsImpacted { get; set; }
        public string TechnicalDependencies { get; set; }
        public double BallParkCost { get; set; }
        public double BallParkDependenciesCost { get; set; }
        public string KeyAssumptions { get; set; }
        public string UserId { get; set; }

        public AddPlanItemRequest(string planId, string categoryName, string name, string businessDefinition, string complexityLevel, string backlogName, int priority, string moScoW, string sizeModelTypeFactorId, string sizeModelTypeValueSelected, string businessFeature, string technicalDefinition, string componentsImpacted, string technicalDependencies, double ballParkCost, double ballParkDependenciesCost, string keyAssumptions, string userId)
        {
            PlanId = planId;
            CategoryName = categoryName;
            Name = name;
            BusinessDefinition = businessDefinition;
            ComplexityLevel = complexityLevel;
            BacklogName = backlogName;
            Priority = priority;
            MoScoW = moScoW;
            SizeModelTypeFactorId = sizeModelTypeFactorId;
            SizeModelTypeValueSelected = sizeModelTypeValueSelected;
            BusinessFeature = businessFeature;
            TechnicalDefinition = technicalDefinition;
            ComponentsImpacted = componentsImpacted;
            TechnicalDependencies = technicalDependencies;
            BallParkCost = ballParkCost;
            BallParkDependenciesCost = ballParkDependenciesCost;
            KeyAssumptions = keyAssumptions;
            UserId = userId;
        }

    }
}
