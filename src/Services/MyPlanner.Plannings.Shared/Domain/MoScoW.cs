namespace BeyondNet.Ddd.ValueObjects
{
    public class MoScoW : Enumeration
    {
        public static MoScoW MustHave = new MoScoW(1, nameof(MustHave).ToLowerInvariant());
        public static MoScoW ShouldHave = new MoScoW(2, nameof(ShouldHave).ToLowerInvariant());
        public static MoScoW CouldHave = new MoScoW(3, nameof(CouldHave).ToLowerInvariant());
        public static MoScoW NiceToHave = new MoScoW(4, nameof(NiceToHave).ToLowerInvariant());
        public static MoScoW WontHave = new MoScoW(5, nameof(WontHave).ToLowerInvariant());
        
        private MoScoW(int id, string name)
            : base(id, name)
        {

        }
    }
}
