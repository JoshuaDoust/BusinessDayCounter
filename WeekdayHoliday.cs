using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowd_Task
{
    // Holiday that occurs on a fixed date except if that date falls on a weekend
    // in which case it moves to the following Monday
    internal class WeekdayHoliday : PublicHoliday
    {
        public WeekdayHoliday(int month, int day)
            : base((DateTime start, DateTime end) =>
            {
                // We assume each full calendar year has exactly one instance of the holiday
                int fullYearsBetween = end.Year - start.Year > 1 ? end.Year - start.Year - 1 : 0;

                DateTime holidayInStartYear = new DateTime(start.Year, month, day);
                if (holidayInStartYear.DayOfWeek == DayOfWeek.Saturday)
                    holidayInStartYear.AddDays(2);
                else if (holidayInStartYear.DayOfWeek == DayOfWeek.Sunday)
                    holidayInStartYear.AddDays(1);

                DateTime holidayInEndYear = new DateTime(end.Year, month, day);
                if (holidayInEndYear.DayOfWeek == DayOfWeek.Saturday)
                    holidayInEndYear.AddDays(2);
                else if (holidayInEndYear.DayOfWeek == DayOfWeek.Sunday)
                    holidayInEndYear.AddDays(1);

                // Check that the year hasn't rolled over to next year and the holiday is in the range
                int additionalHolidaysAfterStart = holidayInStartYear.Year == start.Year &&
                    holidayInStartYear > start ? 1 : 0;
                int additionalHolidaysBeforeEnd = holidayInEndYear.Year == end.Year && 
                    holidayInEndYear < end ? 1 : 0;

                return fullYearsBetween + additionalHolidaysAfterStart + additionalHolidaysBeforeEnd;
            })
        {
        }
    }
}
