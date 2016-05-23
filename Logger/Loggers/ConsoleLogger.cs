using Logger.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void LogMessage(LogCategory logCategory, string message)
        {
            switch (logCategory)
            {
                case LogCategory.Message:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogCategory.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogCategory.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.WriteLine(String.Format("{0} - {1}", logCategory.ToString(), message));
        }
   
    }
}
