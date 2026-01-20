using System.Globalization;

namespace AppView.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly Calendar _gregorianCalendar = new GregorianCalendar();

        public static DateTime FirstDateOfWeekISO8601(int year, int weekNumber)
        {
            var firstOfJan = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Thursday - firstOfJan.DayOfWeek;

            var firstThursdayOfYear = firstOfJan.AddDays(daysOffset);
            var firstWeekOfYear = _gregorianCalendar.GetWeekOfYear(firstThursdayOfYear,
                CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (firstWeekOfYear == 1)
            {
                weekNumber -= 1;
            }

            return firstThursdayOfYear.AddDays(weekNumber * 7 - 3);
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
