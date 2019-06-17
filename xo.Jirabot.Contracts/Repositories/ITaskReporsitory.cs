using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xo.Jirabot.Contracts.Entities.Tasks;

namespace xo.Jirabot.Contracts.Repositories
{
    public interface ITaskReporsitory
    {
        void UpdateStatus(TaskStatus status);

        void ClearPendingTasks();
    }
}
