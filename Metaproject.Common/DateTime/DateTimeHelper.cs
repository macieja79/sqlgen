using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public class DateTimeHelper
    {
        private DateTimeHelper () {}

        public static bool IsDayEqual(DateTime left, DateTime right)
        {
            bool isEqual = 
                left.Year == right.Year &&
                left.Month == right.Month &&
                left.Day == right.Day;

            return isEqual;
        }

        public static DateTime GetNextDayTo(DateTime current, DayOfWeek dayOfWeek)
        {
            const int DAYS_IN_WEEK = 7;

            for (int i = 0; i < DAYS_IN_WEEK; i++)
            {
                var dt = current.AddDays(i);
                if (dt.DayOfWeek == dayOfWeek) return dt;

            }

            return current;
        }

    }
}
