namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class BusinessFeatureProps : IProps
    {
        public string Name { get; private set; }
        public string BusinessDefinition { get; private set; }
        public ComplexityLevelEnum ComplexityLevel { get; private set; }
        public PriorityOrder PriorityOrder { get; private set; }
        public MoScoWEnum MoScoW { get; private set; }

        public BusinessFeatureProps(string name, string businessDefinition, ComplexityLevelEnum complexityLevel, PriorityOrder priorityOrder, MoScoWEnum moScoW)
        {
            Name = name;
            BusinessDefinition = businessDefinition;
            ComplexityLevel = complexityLevel;
            PriorityOrder = priorityOrder;
            MoScoW = moScoW;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class BusinessFeature : ValueObject<BusinessFeatureProps>
    {
        public BusinessFeature(BusinessFeatureProps props) : base(props)
        {
        }

        public static BusinessFeature Create(string name, string businessDefinition, ComplexityLevelEnum complexityLevel, PriorityOrder priorityOrder, MoScoWEnum moScoW)
        {
            return new BusinessFeature(new BusinessFeatureProps(name, businessDefinition, complexityLevel, priorityOrder, moScoW));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return string.Concat(GetValue().Name, GetValue().BusinessDefinition, GetValue().ComplexityLevel, GetValue().PriorityOrder, GetValue().MoScoW);
        }
    }
}
