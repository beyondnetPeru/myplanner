namespace MyPlanner.Shared.Helpers
{
    public static class TimeStampHelper
    {
        public static string GetTimeStamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        }
    }
}
