using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Contracts.Entities.Tasks
{
    public enum TaskStatus
    {
        NONE = 0,
        PLANNED = 1,
        STARTED = 2,
        COMPLETED = 3,
        FAILED = 4,
        RESTARTED = 5,
        REJECTED = 6
    }
}
