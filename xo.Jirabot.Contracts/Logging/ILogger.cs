using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo.Jirabot.Contracts.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Writes the debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteDebug(string message);

        /// <summary>
        /// Writes the info message.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteInfo(string message);

        /// <summary>
        /// Writes the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteError(string message);
    }
}
