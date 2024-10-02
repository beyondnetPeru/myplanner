namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class BusinessFeatureProps : IProps
    {
        public string Name { get; private set; }
        public string BusinessDefinition { get; private set; }
        public string ComplexityLevel { get; private set; }
        public int Priority { get; private set; }
        public string MoScoW { get; private set; }

        public BusinessFeatureProps(string name, string businessDefinition, string complexityLevel, int priority, string moScoW)
        {
            Name = name;
            BusinessDefinition = businessDefinition;
            ComplexityLevel = complexityLevel;
            Priority = priority;
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

        public static BusinessFeature Create(string name, string businessDefinition, string complexityLevel, int priority, string moScoW)
        {
            return new BusinessFeature(new BusinessFeatureProps(name, businessDefinition, complexityLevel, priority, moScoW));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return string.Concat(GetValue().Name, GetValue().BusinessDefinition, GetValue().ComplexityLevel, GetValue().Priority, GetValue().MoScoW);
        }
    }
}
