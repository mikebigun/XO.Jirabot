using System;

namespace xo.Jirabot.Contracts.Entities.Log
{
    public class Log : IEntity
    {
        public int Id { get; set; }

        public DateTime LogDate { get; set; }

        public int LogType { get; set; }

        public string LogMessage { get; set; }
    }
}
