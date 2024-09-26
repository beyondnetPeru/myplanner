using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class BusinessFeatureProps : IProps
    {
        public string CategoryName { get; internal set; }
        public string Name { get; private set; }
        public string BusinessDefinition { get; private set; }
        public string ComplexityLevel { get; private set; }
        public string BacklogName { get; private set; }
        public int Priority { get; private set; }
        public string MoScoW { get; set; }

        public BusinessFeatureProps(string categoryName, string name, string businessDefinition, string complexityLevel, string backlogName, int priority, string moScoW)
        {
            CategoryName = categoryName;
            Name = name;
            BusinessDefinition = businessDefinition;
            ComplexityLevel = complexityLevel;
            BacklogName = backlogName;
            Priority = priority;
            MoScoW = moScoW;
        }

        public object Clone()
        {
            return new BusinessFeatureProps(CategoryName, Name, BusinessDefinition, ComplexityLevel, BacklogName, Priority, MoScoW);
        }
    }

    public class BusinessFeature : ValueObject<BusinessFeatureProps>
    {
        public BusinessFeature(BusinessFeatureProps props) : base(props)
        {
        }

        public static BusinessFeature Create(string categoryName, string name, string businessDefinition, string complexityLevel, string backlogName, int priority, string moScoW)
        {
            return new BusinessFeature(new BusinessFeatureProps(categoryName, name, businessDefinition, complexityLevel, backlogName, priority, moScoW));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return string.Concat(GetValue().CategoryName, GetValue().Name, GetValue().BusinessDefinition, GetValue().ComplexityLevel, GetValue().BacklogName, GetValue().Priority, GetValue().MoScoW);
        }
    }

}
