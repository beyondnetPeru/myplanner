using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelProfileProp : IProps
    {
        public ProfileName ProfileName { get; set; } = ProfileName.Defaultvalue();
        public ProfileAvgRate ProfileAvgRate { get; set; } = ProfileAvgRate.Defaultvalue();

        public object Clone()
        {
            return new SizeModelProfileProp
            {
                ProfileName = ProfileName,
                ProfileAvgRate = ProfileAvgRate,
            };
        }
    }

    public class SizeModelProfile : ValueObject<SizeModelProfileProp>
    {
        private SizeModelProfile(SizeModelProfileProp value) : base(value)
        {
        }

        public static SizeModelProfile Create(ProfileName name, ProfileAvgRate rate)
        {
            var prop = new SizeModelProfileProp()
            {
                ProfileName = name,
                ProfileAvgRate = rate
            };

            return new SizeModelProfile(prop);
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Value.ProfileName + this.Value.ProfileAvgRate.ToString();
        }

    }
}
