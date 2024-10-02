
namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class Description : StringRequiredValueObject
    {
        private Description(string value) : base(value)
        {
        }

        public static new Description Create(string value)
        {
            return new Description(value);
        }

        public static Description DefaultValue => new Description(string.Empty);
    }
}
