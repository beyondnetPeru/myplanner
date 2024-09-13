namespace BeyondNet.Ddd.ValueObjects
{
    public class ConditionalFlag : Enumeration
    {
        public static ConditionalFlag Yes = new ConditionalFlag(1, "Yes");
        public static ConditionalFlag No = new ConditionalFlag(2, "No");

        public ConditionalFlag(int id, string name) : base(id, name)
        {
        }
    }
}
