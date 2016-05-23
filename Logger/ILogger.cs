using Logger.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILogger
    {
        /// <summary>
        /// Write the log message
        /// </summary>
        /// <param name="logCategory">The log category, could be message, warning or error</param>
        /// <param name="message">The message to log</param>
        void LogMessage(LogCategory logCategory, string message);
    }
}
