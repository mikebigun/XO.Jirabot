using System;
using Chronic;

namespace xo.Jirabot.Engine.Helpers
{
    public class FrequencyHelper
    {
        public static bool IsTimeToPlan(string frequency, DateTime lastRun, out DateTime plannedRun)
        {
            var parser = new Parser();

            var planned = parser.Parse(frequency, new Options
            {
                 Context = Pointer.Type.Future,
                 Clock = new Func<DateTime>(() => { return lastRun; }),
                 FirstDayOfWeek = DayOfWeek.Monday                 
            });

            if (!planned.End.HasValue)
            {
                throw new InvalidOperationException("Unable to calculate planned date/time");
            }

            plannedRun = planned.End.Value;

            if (plannedRun > DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}
