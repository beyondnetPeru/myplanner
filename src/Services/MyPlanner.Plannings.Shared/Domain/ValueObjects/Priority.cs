using BeyondNet.Ddd;

namespace BeyondNet.Ddd.ValueObjects
{
    public class Priority : ValueObject<int>
    {
        public static Priority DefaultValue => new Priority(0);

        private Priority(int value) : base(value)
        {
        }

        public static implicit operator Priority(int value) => new Priority(value);

        public static implicit operator int(Priority priority) => priority.Value;

        public static Priority operator +(Priority priority, int value) => new Priority(priority.Value + value);

        public static Priority operator -(Priority priority, int value) => new Priority(priority.Value - value);

        public static Priority operator ++(Priority priority) => new Priority(priority.Value + 1);

        public static Priority operator --(Priority priority) => new Priority(priority.Value - 1);

        public static Priority FromValue(int value) => new Priority(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
