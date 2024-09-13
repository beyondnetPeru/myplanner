using BeyondNet.Ddd;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelStatusEnum : Enumeration
    {
        public static SizeModelStatusEnum Active = new SizeModelStatusEnum(1, "Active");
        public static SizeModelStatusEnum Inactive = new SizeModelStatusEnum(2, "Inactive");
        public SizeModelStatusEnum(int id, string name) : base(id, name)
        {
        }
    }

    public class SizeModelTypeEnum : Enumeration
    {
        public static SizeModelTypeEnum Default = new SizeModelTypeEnum(1, "Default");
        public static SizeModelTypeEnum TShirt = new SizeModelTypeEnum(2, "TShirt");
        public SizeModelTypeEnum(int id, string name) : base(id, name)
        {
        }
    }
    public class TShirtSizeEnum : Enumeration
    {
        public static TShirtSizeEnum XS = new TShirtSizeEnum(1, "XS");
        public static TShirtSizeEnum S = new TShirtSizeEnum(2, "S");
        public static TShirtSizeEnum M = new TShirtSizeEnum(3, "M");
        public static TShirtSizeEnum L = new TShirtSizeEnum(4, "L");
        public static TShirtSizeEnum XL = new TShirtSizeEnum(5, "XL");
        public static TShirtSizeEnum XXL = new TShirtSizeEnum(6, "XXL");

        public TShirtSizeEnum(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<TShirtSizeEnum> List() =>
            new[] { XS, S, M, L, XL, XXL };

        public static TShirtSizeEnum FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for TShirtSize: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static TShirtSizeEnum From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Possible values for TShirtSize: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
