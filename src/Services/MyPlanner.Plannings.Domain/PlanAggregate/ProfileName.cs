namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class ProfileName : StringValueObject
    {
        private ProfileName(string value) : base(value)
        {
        }

        public static ProfileName Create(string value)
        {
            return new ProfileName(value);
        }

        public static ProfileName Defaultvalue()
        {
            return new ProfileName("Default");
        }
    }
}
