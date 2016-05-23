using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            JobLogger logger = new JobLogger();
            logger.AddLogger(new Logger.Loggers.ConsoleLogger());
            logger.AddLogger(new Logger.Loggers.FileLogger());
            logger.AddLogCategory(Enums.LogCategory.Error);
            logger.LogMessage(Enums.LogCategory.Error, "Test");
        }
    }
}
