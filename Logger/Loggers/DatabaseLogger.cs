using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Loggers
{
    public class DatabaseLogger:ILogger
    {
        public void LogMessage(Enums.LogCategory logCategory, string message)
        {
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"];
                var providerName = connectionString.ProviderName;
                var factory = DbProviderFactories.GetFactory(providerName);

                try
                {
                    using (IDbConnection connection = factory.CreateConnection())
                    {
                        connection.ConnectionString = connectionString.ConnectionString;
                        connection.Open();
                        using (IDbCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "insert into Log values(@message, @type)";

                            var parameterMessage = command.CreateParameter();
                            parameterMessage.ParameterName = "@message";
                            parameterMessage.Value = message;
                            command.Parameters.Add(parameterMessage);

                            var parameterType = command.CreateParameter();
                            parameterType.ParameterName = "@type";
                            parameterType.Value = logCategory.ToString();
                            command.Parameters.Add(parameterType);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }


            }
        }
    }
}
