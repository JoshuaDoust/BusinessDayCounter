using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowd_Task
{
    [TestFixture]
    internal class BusinessDayCounterTests
    {
        private BusinessDayCounter businessDayCounter = new();

        [Test]
        public void ShouldProduceExpectedWeekdays()
        {
            IList<DateTime> startDates = new List<DateTime> { 
                new DateTime(2013, 10, 7),
                new DateTime(2013, 10, 5),
                new DateTime(2013, 10, 7),
                new DateTime(2013, 10, 7)
            };

            IList<DateTime> endDates = new List<DateTime> {
                new DateTime(2013, 10, 9),
                new DateTime(2013, 10, 14),
                new DateTime(2014, 1, 1),
                new DateTime(2013, 10, 5)
            };

            IList<int> expectedDays = new List<int> {
                1, 5, 61, 0
            };

            for (var i = 0; i < expectedDays.Count; i++)
            {
                int weekdays = businessDayCounter.WeekdaysBetweenTwoDates(startDates[i], endDates[i]);
                Assert.AreEqual(expectedDays[i], weekdays);
            }
        }

        [Test]
        public void ShouldProduceExpectedBusinessDays()
        {
            IList<DateTime> testPublicHolidays = new List<DateTime> {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };

            IList<DateTime> startDates = new List<DateTime> {
                new DateTime(2013, 10, 7),
                new DateTime(2013, 12, 24),
                new DateTime(2013, 10, 7)
            };

            IList<DateTime> endDates = new List<DateTime> {
                new DateTime(2013, 10, 9),
                new DateTime(2013, 12, 27),
                new DateTime(2014, 1, 1)
            };

            IList<int> expectedDays = new List<int> {
                1, 0, 59
            };

            for (var i = 0; i < expectedDays.Count; i++)
            {
                int weekdays = businessDayCounter.BusinessDaysBetweenTwoDates(
                    startDates[i], 
                    endDates[i], 
                    testPublicHolidays
                );
                Assert.AreEqual(expectedDays[i], weekdays);
            }
        }

        [Test]
        public void ShouldProduceExpectedBusinessDaysWithHolidayClass()
        {
            IList<PublicHoliday> testPublicHolidays = new List<PublicHoliday> {
                new FixedPublicHoliday(12, 25),
                new WeekdayHoliday(12, 26),
                new WeekdayHoliday(4, 8),
                new DayIndexHoliday(1, DayOfWeek.Tuesday, 1),
                new DayIndexHoliday(5, DayOfWeek.Monday, 2)
            };

            IList<DateTime> startDates = new List<DateTime> {
                new DateTime(2013, 10, 7),
                new DateTime(2013, 12, 24),
                new DateTime(2013, 10, 7),
                new DateTime(2023, 4, 30),
                new DateTime(2023, 4, 9)
            };

            IList<DateTime> endDates = new List<DateTime> {
                new DateTime(2013, 10, 9),
                new DateTime(2013, 12, 27),
                new DateTime(2014, 1, 1),
                new DateTime(2023, 5, 13),
                new DateTime(2023, 4, 16)
            };

            IList<int> expectedDays = new List<int> {
                1, 0, 59, 9, 4
            };

            for (var i = 0; i < expectedDays.Count; i++)
            {
                int weekdays = businessDayCounter.BusinessDaysBetweenTwoDates(
                    startDates[i],
                    endDates[i],
                    testPublicHolidays
                );
                Assert.AreEqual(expectedDays[i], weekdays);
            }
        }
    }
}
