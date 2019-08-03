using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Contracts.Entities.Tasks
{
    public enum TaskStatus
    {
        PLANNED = 1,
        COMPLETED = 2,
        FAILED = 3,
        CANCELED = 4
    }
}
