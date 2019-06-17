using System;
using System.Data;
using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities.Tasks;
using xo.Jirabot.Contracts.Repositories;

namespace xo.Jirabot.Data.Repositories
{
    public class TaskRepository : BaseRepository<Task>, ITaskReporsitory
    {
        public TaskRepository(ITargetDatabase database) : base(database)
        {
        }

        public void ClearPendingTasks()
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(TaskStatus status)
        {
            throw new NotImplementedException();
        }

        protected override Task MapEntity(IDataRecord record)
        {
            throw new NotImplementedException();
        }
    }
}
