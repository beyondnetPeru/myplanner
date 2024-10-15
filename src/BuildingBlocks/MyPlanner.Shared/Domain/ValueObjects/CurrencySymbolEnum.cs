namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class CurrencySymbolEnum : Enumeration
    {

        public CurrencySymbolEnum(int id, string name) : base(id, name)
        {
        }

        public static CurrencySymbolEnum USD = new CurrencySymbolEnum(1, "USD");
        public static CurrencySymbolEnum EUR = new CurrencySymbolEnum(2, "EUR");
        public static CurrencySymbolEnum GBP = new CurrencySymbolEnum(3, "GBP");
        public static CurrencySymbolEnum JPY = new CurrencySymbolEnum(4, "JPY");
        public static CurrencySymbolEnum CAD = new CurrencySymbolEnum(5, "CAD");
        public static CurrencySymbolEnum AUD = new CurrencySymbolEnum(6, "AUD");
        public static CurrencySymbolEnum CHF = new CurrencySymbolEnum(7, "CHF");
        public static CurrencySymbolEnum CNY = new CurrencySymbolEnum(8, "CNY");
        public static CurrencySymbolEnum SEK = new CurrencySymbolEnum(9, "SEK");
        public static CurrencySymbolEnum NZD = new CurrencySymbolEnum(10, "NZD");
        public static CurrencySymbolEnum KRW = new CurrencySymbolEnum(11, "KRW");
        public static CurrencySymbolEnum SGD = new CurrencySymbolEnum(12, "SGD");
        public static CurrencySymbolEnum NOK = new CurrencySymbolEnum(13, "NOK");
        public static CurrencySymbolEnum MXN = new CurrencySymbolEnum(14, "MXN");
        public static CurrencySymbolEnum INR = new CurrencySymbolEnum(15, "INR");
        public static CurrencySymbolEnum RUB = new CurrencySymbolEnum(16, "RUB");
        public static CurrencySymbolEnum ZAR = new CurrencySymbolEnum(17, "ZAR");
        public static CurrencySymbolEnum BRL = new CurrencySymbolEnum(18, "BRL");
        public static CurrencySymbolEnum HKD = new CurrencySymbolEnum(19, "HKD");
        public static CurrencySymbolEnum IDR = new CurrencySymbolEnum(20, "IDR");
        public static CurrencySymbolEnum MYR = new CurrencySymbolEnum(21, "MYR");
        public static CurrencySymbolEnum TRY = new CurrencySymbolEnum(22, "TRY");
        public static CurrencySymbolEnum SAR = new CurrencySymbolEnum(23, "SAR");
        public static CurrencySymbolEnum AED = new CurrencySymbolEnum(24, "AED");
        public static CurrencySymbolEnum THB = new CurrencySymbolEnum(25, "THB");
        public static CurrencySymbolEnum DKK = new CurrencySymbolEnum(26, "DKK");
        public static CurrencySymbolEnum PLN = new CurrencySymbolEnum(27, "PLN");
        public static CurrencySymbolEnum HUF = new CurrencySymbolEnum(28, "HUF");

    }
}
