using System;
using xo.Jirabot.Contracts;

namespace xo.Jirabot.Contracts.Entities
{
    public class Log : IEntity
    {
        public int Id { get; set; }

        public int LogType { get; set; }

        public string LogMessage { get; set; }

        public DateTime LogDate { get; set; }
    }
}
