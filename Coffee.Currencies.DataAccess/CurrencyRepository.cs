using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coffee.Shared.Configuration;

namespace Coffee.Currencies.DataAccess
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IConfigurationService _configurationService;

        public CurrencyRepository(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public IEnumerable<string> EnumerateCurrency()
        {
            var result = new List<string>();
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT CurrencyCode FROM Currencies";
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add((string) reader["CurrencyCode"]);
                        }
                    }
                }
            }

            return result;
        }

        public bool CreateCurrency(string currencyCode)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_CreateCurrency";
                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode).SqlDbType = SqlDbType.NVarChar;
                    var returnParameter = command.Parameters.Add("@Return", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)returnParameter.Value == 1;
                }
            }
        }

        public string DeleteCurrency(string currencyCode)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_DeleteCurrency";
                    command.Parameters.AddWithValue("@CurrencyCode", currencyCode).SqlDbType = SqlDbType.NVarChar;
                    var returnParameter = command.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    switch ((int)returnParameter.Value)
                    {
                        case 1:
                            return null;
                        default:
                            return "Имеются записи связанные с данной валютой";
                    }
                }
            }
        }
    }
}
