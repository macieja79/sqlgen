namespace System
{
    public static class DateTimeExtensions
    {
        public static bool IsTheSameDay(this DateTime dateTime, DateTime other)
        {
            return dateTime.Year == other.Year &&
                   dateTime.Month == other.Month &&
                   dateTime.Day == other.Day;
        }

        public static DateTime DayStart(this DateTime dateTime)
        {
            DateTime result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return result;
        }

        public static DateTime DayEnd(this DateTime dateTime)
        {
            DateTime result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999);
            return result;
        }

        public static bool IsMin(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue;
        }

        public static bool IsMax(this DateTime dateTime)
        {
            return dateTime == DateTime.MaxValue;
        }

        public static string ToDateLiteral(this DateTime dt)
        {
            return $"{dt.Year}{dt.Month:00}{dt.Day:00}";
        }

        public static string ToDateTimeStamp(this DateTime dt)
        {
            string stamp = $"{dt.Year}{dt.Month:00}{dt.Day:00}{dt.Hour:00}{dt.Minute:00}";
            return stamp;
        }

        public static DateTime TrimTime(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }

        public static DateTime Yesterday(this DateTime dt)
        {
            var yestarday = dt.AddDays(-1);
            return yestarday;
        }

        public static DateTime Tommorow(this DateTime dt)
        {
            var yestarday = dt.AddDays(+1);
            return yestarday;
        }

        public static bool IsLastDayOfMonth(this DateTime dt)
        {
            int daysOfMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            bool isLastDay = dt.Day == daysOfMonth;
            return isLastDay;
        }

        public static DateTime? ToNullable(this DateTime t)
        {
            return t;
        }

        public static DateTime ValueOrNow(this DateTime? t)
        {
            if (t.HasValue)
                return t.Value;

            return DateTime.Now;
        }

        public static DateTime WithTime(this DateTime dt, int hours, int minutes, int seconds)
        {
           
            return new DateTime(dt.Year, dt.Month, dt.Day, hours, minutes, seconds);
        }


    }
}

