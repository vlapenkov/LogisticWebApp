using Logistic.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Web.Services
{
    public class SqlHelper
    {
        private static readonly  int maxLength = 500;
        private string ConnectionString { get; set; }

        public SqlHelper(string connectionStr)
        {
            ConnectionString = connectionStr;
        }

        private bool ExecuteNonQuery(string commandStr, List<SqlParameter> paramList)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand command = new SqlCommand(commandStr, conn))
                {

                    try
                    {
                        command.Parameters.AddRange(paramList.ToArray());
                        int count = command.ExecuteNonQuery();
                        result = count > 0;
                    }
                    catch { }
                    

                    
                }
            }
            return result;
        }

        public bool InsertLog(EventLog log)
        {
            string command = $@"INSERT INTO [dbo].[EventLogs] ([EventID],[LogLevel],[Message],[CreatedTime]) VALUES (@EventID, @LogLevel, @Message, @CreatedTime)";
            var paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("EventID", log.EventId));
            paramList.Add(new SqlParameter("LogLevel", log.LogLevel));
            
            var length= log.Message.Length > maxLength ? maxLength : log.Message.Length;

            paramList.Add(new SqlParameter("Message", log.Message.Substring(0, length)));
            paramList.Add(new SqlParameter("CreatedTime", log.CreatedTime));
            return ExecuteNonQuery(command, paramList);
        }
    }

}
