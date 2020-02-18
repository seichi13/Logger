using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Loggers
{
    public class FileLogger:ILogger
    {
        public void LogMessage(Enums.LogCategory logCategory, string message)
        {
            string fileName = ConfigurationManager.AppSettings["LogFileDirectory"] +"LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            try
            {
                using(StreamWriter file = new StreamWriter(fileName,true)){
                    file.WriteLine(string.Format("{0} - {1}", logCategory.ToString(), message));
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
