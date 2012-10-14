using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Coffee.News.Domain;
using Coffee.Shared.Configuration;

namespace Coffee.News.DataAccess
{
    public class NewsRepository : INewsRepository
    {
        private readonly IConfigurationService _configurationService;

        public NewsRepository(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public IEnumerable<ShortNews> EnumerateNews(int startNumberRow, int endNumberRow)
        {
            var result = new List<ShortNews>();
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT RN, CreatedAt, Description," +
                                          " NewsId, Subject, UpdatedAt FROM vw_News " +
                                          "WHERE RN BETWEEN @StartNumberRow AND @EndNumberRow";
                    command.Parameters.AddWithValue("@StartNumberRow", startNumberRow).SqlDbType = SqlDbType.Int;
                    command.Parameters.AddWithValue("@EndNumberRow", endNumberRow).SqlDbType = SqlDbType.Int;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ShortNews
                                           {
                                               CreatedAt = (DateTime)reader["CreatedAt"],
                                               Description = (string)reader["Description"],
                                               NewsId = (Guid)reader["NewsId"],
                                               Subject = (string)reader["Subject"],
                                               UpdatedAt = (DateTime)reader["UpdatedAt"]
                                           });
                        }
                    }

                    return result;
                }
            }
        }

        public FullNews GetNews(Guid newsId)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT CreatedAt, Description, NewsId, Subject, UpdatedAt, Content FROM News WHERE NewsId = @NewsId";
                    command.Parameters.AddWithValue("@NewsId", newsId).SqlDbType = SqlDbType.UniqueIdentifier;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FullNews
                            {
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                Description = (string)reader["Description"],
                                NewsId = (Guid)reader["NewsId"],
                                Subject = (string)reader["Subject"],
                                UpdatedAt = (DateTime)reader["UpdatedAt"],
                                Content = (string)reader["Content"]
                            };
                        }

                        return null;
                    }
                }
            }
        }

        public Guid CreateNews(string content, string subject, string description)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_CreateNews";
                    command.Parameters.AddWithValue("@Сontent", content).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Subject", subject).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Description", description).SqlDbType = SqlDbType.NVarChar;
                    var returnParameter = command.Parameters.Add("@NewsId", SqlDbType.UniqueIdentifier);
                    returnParameter.Direction = ParameterDirection.Output;
                    connection.Open();
                    command.ExecuteNonQuery();
                    if (returnParameter.Value == DBNull.Value)
                    {
                        throw new InvalidOperationException("Не удалось создать новость");
                    }

                    return (Guid)returnParameter.Value;
                }
            }
        }

        public bool UpdateNews(Guid newsId, string content, string subject, string description)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_UpdateNews";
                    command.Parameters.AddWithValue("@NewsId", newsId).SqlDbType = SqlDbType.UniqueIdentifier;
                    command.Parameters.AddWithValue("@Сontent", content).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Subject", subject).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Description", description).SqlDbType = SqlDbType.NVarChar;
                    var returnParameter = command.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)returnParameter.Value == 1;
                }
            }
        }

        public bool DeleteNews(Guid newsId)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_DeleteNews";
                    command.Parameters.AddWithValue("@NewsId", newsId).SqlDbType = SqlDbType.UniqueIdentifier;
                    var returnParameter = command.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)returnParameter.Value == 1;
                }
            }
        }
    }
}