using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowd_Task
{
    internal class FixedPublicHoliday : PublicHoliday
    {
        private static bool IsWeekDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        public FixedPublicHoliday(int month, int day) 
            : base((DateTime start , DateTime end) =>
            {
                DateTime holidayInStartYear = new DateTime(start.Year, month, day);
                DateTime holidayInEndYear = new DateTime(end.Year, month, day);

                int holidaysInYearsBetween = 0;
                for (var year = start.Year + 1; year < end.Year; year++)
                {
                    if (IsWeekDay(new DateTime(year, month, day)))
                        holidaysInYearsBetween++;
                }

                int additionalHolidaysAfterStart = holidayInStartYear > start && holidayInStartYear < end ? 1 : 0;
                int additionalHolidaysBeforeEnd = holidayInEndYear > start && holidayInEndYear < end ? 1 : 0;

                int totalHolidays;
                if (start.Year != end.Year)
                    totalHolidays = holidaysInYearsBetween + additionalHolidaysAfterStart + additionalHolidaysBeforeEnd;
                else
                    totalHolidays = holidaysInYearsBetween + additionalHolidaysAfterStart;

                return totalHolidays;
            })
        {
        }
    }
}
