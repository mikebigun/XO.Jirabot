using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronic;
using xo.Jirabot.Contracts.Globals;

namespace xo.Jirabot.Engine.Helpers
{
    public class FrequencyHelper
    {
        public static bool IsTimeToPlan(string frequency, string lastRun, out string plannedRun)
        {
            var last = DateTime.MinValue;

            if (!DateTime.TryParse(lastRun, out last))
            {
                throw new InvalidOperationException("Invalid last run date");
            }

            var parser = new Parser();

            var planned = parser.Parse(frequency, new Options
            {
                 Context = Pointer.Type.Future,
                 Clock = new Func<DateTime>(() => { return last; })
            });

            if (!planned.End.HasValue)
            {
                throw new InvalidOperationException("Invalid planned date");
            }

            plannedRun = planned.End.Value.ToString(Constants.DateTimeFormat);

            if (planned.End.Value > DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}
