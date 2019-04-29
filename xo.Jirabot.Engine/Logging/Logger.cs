using System;
using xo.Jirabot.Contracts;
using xo.Jirabot.Contracts.Entities;
using xo.Jirabot.Contracts.Logging;

namespace xo.Jirabot.Engine.Logging
{
    public class Logger : ILogger
    {
        private IRepository<Log> __repo = null;

        public Logger()
        {
            __repo = EngineContext.Instance().Factory.CreateRepository<Log>();
        }

        public void WriteDebug(string message)
        {
            Write(message, LogType.DEBUG);
        }

        public void WriteInfo(string message)
        {
            Write(message, LogType.INFO);
        }

        public void WriteError(string message)
        {
            Write(message, LogType.ERROR);
        }

        private void Write(string message, LogType type)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                __repo.Create(new Log()
                {
                    LogType = (int)type,
                    LogMessage = message,
                    LogDate = DateTime.Now
                });
            }
        }
    }
}
