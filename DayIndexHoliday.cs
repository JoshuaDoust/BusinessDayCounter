using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowd_Task
{
    internal class DayIndexHoliday : PublicHoliday
    {
        private static DateTime FindHolidayInYear(int year, int month, DayOfWeek dayOfWeek, int dayIndex)
        {
            DateTime holidayInYear = new DateTime(year, month, 1);
            int foundDaysInStartYear = 0;
            while (true)
            {
                if (holidayInYear.DayOfWeek == dayOfWeek)
                    foundDaysInStartYear++;

                if (foundDaysInStartYear < dayIndex)
                    holidayInYear.AddDays(1);
                else
                    break;
            }

            return holidayInYear;
        }

        // For holidays on the {dayIndex}'th {dayOfWeek} in {month}
        // dayIndex is 1 indexes - ie. the second Monday would have dayIndex = 2
        public DayIndexHoliday(int month, DayOfWeek dayOfWeek, int dayIndex)
            : base((DateTime start, DateTime end) =>
            {
                // We assume each full calendar year has exactly one instance of the holiday
                int fullYearsBetween = end.Year - start.Year > 1 ? end.Year - start.Year - 1 : 0;

                DateTime holidayInStartYear = FindHolidayInYear(start.Year, month, dayOfWeek, dayIndex);

                DateTime holidayInEndYear = FindHolidayInYear(end.Year, month, dayOfWeek, dayIndex);

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
