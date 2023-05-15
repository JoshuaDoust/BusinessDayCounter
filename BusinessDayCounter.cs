using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowd_Task
{
    internal class BusinessDayCounter
    {
        const int DAYS_IN_WEEK = 7;
        const int WEEKDAYS_IN_WEEK = 5;

        private bool IsWeekDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (secondDate <= firstDate)
                return 0;

            DateTime dayAfterFirstDate = firstDate.AddDays(1);
            int fullWeeks = (secondDate - dayAfterFirstDate).Days / DAYS_IN_WEEK;
            int additionalDays = 0;
            for (int i = (int)dayAfterFirstDate.DayOfWeek; i != (int)secondDate.DayOfWeek; i = (i + 1) % DAYS_IN_WEEK)
            {
                if (i != (int)DayOfWeek.Saturday && i != (int)DayOfWeek.Sunday)
                    additionalDays++;
            }
            return fullWeeks * WEEKDAYS_IN_WEEK + additionalDays;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            if (secondDate <= firstDate)
                return 0;

            int businessDaysBetweenDates = WeekdaysBetweenTwoDates(firstDate, secondDate);

            foreach (var holiday in publicHolidays)
            {
                if (IsWeekDay(holiday) && holiday > firstDate && holiday < secondDate)
                    businessDaysBetweenDates--;
            }

            return businessDaysBetweenDates;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)
        {
            if (secondDate <= firstDate)
                return 0;

            int businessDaysBetweenDates = WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Note that this assumes that the holidays provided are mutually exclusive
            foreach (var holiday in publicHolidays)
            {
                int holidaysInInterval = holiday.GetInstanceCountInInterval(firstDate, secondDate);
                businessDaysBetweenDates -= holidaysInInterval;
            }

            return businessDaysBetweenDates;
        }
    }
}
