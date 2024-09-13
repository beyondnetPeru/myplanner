using BeyondNet.Ddd.ValueObjects;

namespace MyPlanner.Plannings.Shared.Domain.ValueObjects
{
    public class Owner : StringRequiredValueObject
    {
        protected Owner(string value) : base(value)
        {
        }

        public static Owner Create(string value)
        {
            return new Owner(value);
        }

        public static new Owner DefaultValue => new Owner(string.Empty);
    }
}
