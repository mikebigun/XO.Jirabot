using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Engine.Helpers
{
    public class FrequencyHelper
    {
        public static int MinutesDiffToNow(long ticks)
        {
            var now = DateTime.Now.Ticks;

            if (now >= ticks)
            {
                return (int)TimeSpan.FromTicks(now - ticks).TotalMinutes;
            }

            return -1;
        }
    }
}
