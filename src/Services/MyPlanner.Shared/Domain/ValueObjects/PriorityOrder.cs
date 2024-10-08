using BeyondNet.Ddd;

namespace BeyondNet.Ddd.ValueObjects
{
    public class PriorityOrder : ValueObject<int>
    {
        public static PriorityOrder DefaultValue => new PriorityOrder(0);

        private PriorityOrder(int value) : base(value)
        {
        }

        public static implicit operator PriorityOrder(int value) => new PriorityOrder(value);

        public static implicit operator int(PriorityOrder priority) => priority.Value;

        public static PriorityOrder operator +(PriorityOrder priority, int value) => new PriorityOrder(priority.Value + value);

        public static PriorityOrder operator -(PriorityOrder priority, int value) => new PriorityOrder(priority.Value - value);

        public static PriorityOrder operator ++(PriorityOrder priority) => new PriorityOrder(priority.Value + 1);

        public static PriorityOrder operator --(PriorityOrder priority) => new PriorityOrder(priority.Value - 1);

        public static PriorityOrder FromValue(int value) => new PriorityOrder(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
