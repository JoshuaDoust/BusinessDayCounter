using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowd_Task
{
    internal abstract class PublicHoliday
    {
        // Note that these should only include holidays that fall on business days.
        private readonly Func<DateTime, DateTime, int> getInstanceCountInInterval;

        protected PublicHoliday(Func<DateTime, DateTime, int> getInstanceCountInInterval)
        {
            this.getInstanceCountInInterval = getInstanceCountInInterval;
        }

        public int GetInstanceCountInInterval(DateTime startDate, DateTime endDate)
        {
            return getInstanceCountInInterval(startDate, endDate);
        }
    }
}
