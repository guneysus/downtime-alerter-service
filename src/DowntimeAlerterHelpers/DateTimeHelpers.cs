using System;

namespace DowntimeAlerterHelpers
{
    public static class DateTimeHelpers
    {
        static TimeSpan getTime(this DateTime datetime) => datetime.Subtract(datetime.Date);

        static DateTime getNextExecution(this DateTime datetime, TimeSpan after) => datetime.Add(after);

        public static int getNextExecutionTimeTotalMinutes(this DateTime datetime, TimeSpan after) => getNextExecution(datetime, after)
            .getTime()
            .TotalMinutes.toInt();

        static int toInt(this double value) => Convert.ToInt32(value);
    }

}
