using System;
using System.Collections.Generic;
using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities;

namespace xo.Jirabot.Data.Repositories
{
    internal class LogRepository : IRepository<Log>
    {
        private ITargetDatabase __database;

        public LogRepository(ITargetDatabase database)
        {
            __database = database;
        }

        public void Create(Log entity)
        {
            __database.ExecuteNonQuery("INSERT INTO Log (LogType, LogMessage, LogDate) VALUES (@LogType, @LogMessage, @LogDate)", 
                new Dictionary<string, object>
                {
                    { "@LogType", entity.LogType },
                    { "@LogMessage", entity.LogMessage },
                    { "@LogDate", entity.LogDate.ToString() }
                });
        }

        public void Delete(Log entity)
        {
            __database.ExecuteNonQuery("DELETE FROM Log WHERE Id = @Id", 
                new Dictionary<string, object>
                {
                    { "@Id", entity.Id }
                });
        }

        public Log GetById(int id)
        {
            using (var dr = __database.ExecuteReader(
                "SELECT Id, LogType, LogMessage, LogDate FROM Log WHERE Id = @Id", new Dictionary<string, object> { { "@Id", id } }))
            {
                if (dr.HasRows)
                {
                    return new Log
                    {
                        Id = dr.GetInt32(0),
                        LogType = dr.GetInt32(1),
                        LogMessage = dr.GetString(2),
                        LogDate = Convert.ToDateTime(dr.GetString(3))
                    };
                }
            }
            return null;
        }

        public IEnumerable<Log> GetMany(string query)
        {
            throw new NotImplementedException();
        }

        public void Update(Log entity)
        {
            __database.ExecuteNonQuery("UPDATE Log SET LogType = @LogType, @LogMessage = @LogMessage, @LogDate = @LogDate WHERE @Id = @Id",
                new Dictionary<string, object>
                {
                    { "@LogType", entity.LogType },
                    { "@LogMessage", entity.LogMessage },
                    { "@LogDate", entity.LogDate.ToString() },
                    { "@Id", entity.Id }
                });
        }
    }
}
