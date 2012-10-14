using System;
using System.Data;
using System.Data.SqlClient;
using Coffee.Shared.Configuration;

namespace Coffee.Shared.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly IConfigurationService _configurationService;

        public LoggingService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public void Log(object component, string message, LogType logType, Exception e = null)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }

            if (!Attribute.IsDefined(component.GetType(), typeof(LoggingIdAttribute)))
            {
                throw new ArgumentOutOfRangeException("component");
            }

            var componentName = ((LoggingIdAttribute)Attribute.GetCustomAttribute(component.GetType(), typeof(LoggingIdAttribute))).LoggingId;

            using (var con = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "sp_InsertIntoLog";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Component", componentName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Type", (int)logType).SqlDbType = SqlDbType.Int;
                    cmd.Parameters.AddWithValue("@Message", message ?? string.Empty).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Exception", e != null ? (object)e.ToByteArray(false) : DBNull.Value).SqlDbType = SqlDbType.VarBinary;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}