using System.Collections.Generic;
using xo.Jirabot.Contracts.Entities;

namespace xo.Jirabot.Contracts.Repositories
{
    public interface ILogRepository : IRepository
    {
        void Create(Log entity);

        IEnumerable<Log> GetTopLogs(int top);
    }
}
