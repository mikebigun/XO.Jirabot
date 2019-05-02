using System;
using xo.Jirabot.Contracts.Entities.Log;
using xo.Jirabot.Contracts.Logging;
using xo.Jirabot.Contracts.Repositories;

namespace xo.Jirabot.Engine.Logging
{
    public class Logger : ILogger
    {
        private ILogRepository __repo = null;

        public Logger()
        {
            __repo = EngineContext.Instance().Factory.Get<ILogRepository>();
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
