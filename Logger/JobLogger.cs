using Logger.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Logger
{
    public class JobLogger
    {

        private List<ILogger> _loggers;
        private List<LogCategory> _logCategories;

        public JobLogger()
        {
            _loggers = new List<ILogger>();
            _logCategories = new List<LogCategory>();

        }

        /// <summary>
        /// A better aproach for a Logger is to keep the Single Responsibility Principle 
        /// </summary>
        /// <param name="logCategory"></param>
        /// <param name="message"></param>
        public void LogMessage(Enums.LogCategory logCategory, string message)
        {
            if (this._loggers.Count == 0)
                throw new Exception("Invalid configuration");

            if (this._logCategories.Count == 0)
                throw new Exception("Error or Warning or Message must be specified");

            if (!String.IsNullOrEmpty(message)
                && this._logCategories.Exists(x => x == logCategory))
            {
                foreach (ILogger logger in _loggers.Distinct())
                {
                    logger.LogMessage(logCategory, message);
                }

            }

        }

        public void AddLogger(ILogger logger)
        {
            if (logger != null )
            {
                _loggers.Add(logger);
            }
        }

        public void AddLogCategory(LogCategory logCategory)
        {
            if (!this._logCategories.Any(x => x == logCategory))
            {
                this._logCategories.Add(logCategory);
            }
        }

        public void ClearLoggers()
        {
            _loggers.Clear();
        }

        public void ClearLogCategories()
        {
            this._logCategories.Clear();
        }

        public int CountLoggers
        {
            get { return this._loggers.Count; }
        }

        public int CountLogCategories
        {
            get { return this._logCategories.Count; }
        }

        public void RemoveLogCategory(LogCategory logCategory)
        {
            this._logCategories.Remove(logCategory);
        }

    }
}
