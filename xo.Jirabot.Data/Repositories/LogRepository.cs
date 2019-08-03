using System;
using System.Collections.Generic;
using System.Data;
using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities.Log;
using xo.Jirabot.Contracts.Repositories;

namespace xo.Jirabot.Data.Repositories
{
    internal class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(ITargetDatabase database) : base(database)
        {
        }

        public void Create(Log entity)
        {
            base.Post("INSERT INTO Log (LogType, LogMessage, LogDate) VALUES (@LogType, @LogMessage, @LogDate)",
                new Dictionary<string, object>
                {
                    { "@LogType", entity.LogType },
                    { "@LogMessage", entity.LogMessage },
                    { "@LogDate", entity.LogDate.ToString() }
                });
        }

        public IEnumerable<Log> GetTopLogs(int top)
        {
            return base.Get($"SELECT Id, LogType, LogMessage, LogDate FROM Log ORDER BY Id DESC LIMIT { top }");
        }

        protected override Log MapEntity(IDataRecord record)
        {
            var entity = new Log
            {
                Id = ValueOrDefault<int>(record, "Id"),
                LogDate = ValueOrDefault<DateTime>(record, "LogDate"),
                LogType = ValueOrDefault<int>(record, "LogType"),
                LogMessage = ValueOrDefault<string>(record, "LogMessage")
            };
            return entity;
        }
    }
}
