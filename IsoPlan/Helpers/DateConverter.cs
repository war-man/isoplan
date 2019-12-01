using System;

namespace IsoPlan.Helpers
{
    public static class DateConverter
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static DateTime UnixToDateTime(double timestamp)
        {
            return Epoch.AddSeconds(timestamp);
        }
        public static double DateTimeToUnix(DateTime date)
        {
            TimeSpan elapsedTime = date - Epoch;
            return elapsedTime.TotalSeconds;
        }
    }
}
