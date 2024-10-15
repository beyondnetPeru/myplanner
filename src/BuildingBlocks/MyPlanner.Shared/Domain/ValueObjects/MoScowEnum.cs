namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class MoScoWEnum : Enumeration
    {
        public static MoScoWEnum MustHave = new MoScoWEnum(1, nameof(MustHave).ToLowerInvariant());
        public static MoScoWEnum ShouldHave = new MoScoWEnum(2, nameof(ShouldHave).ToLowerInvariant());
        public static MoScoWEnum CouldHave = new MoScoWEnum(3, nameof(CouldHave).ToLowerInvariant());
        public static MoScoWEnum NiceToHave = new MoScoWEnum(4, nameof(NiceToHave).ToLowerInvariant());
        public static MoScoWEnum WontHave = new MoScoWEnum(5, nameof(WontHave).ToLowerInvariant());

        private MoScoWEnum(int id, string name)
            : base(id, name)
        {

        }
    }
}
