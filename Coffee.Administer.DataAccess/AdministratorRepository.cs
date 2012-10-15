namespace Coffee.Administer.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Coffee.Account.DataAccess;
    using Coffee.Administer.Domain;
    using Coffee.Shared.Configuration;
    using Coffee.Shared.Helper;

    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly IConfigurationService _configurationService;

        private readonly ISharedHelper _helper;

        private readonly IAccountRepository _repository;

        public AdministratorRepository(IConfigurationService configurationService, ISharedHelper helper, IAccountRepository repository)
        {
            _configurationService = configurationService;
            _helper = helper;
            _repository = repository;
        }

        #region Implementation of IAdministratorRepository

        public bool CreateUser(UserInfo userInfo)
        {
            var salt = _helper.GenerateSalt();
            var pas = _helper.EncodePassword(userInfo.Password, salt);
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_CreateUser";
                    command.Parameters.AddWithValue("@Login", userInfo.Login).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Password", pas).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@PasswordSalt", salt).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Email", userInfo.Email).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Fio", userInfo.Fio).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Address", userInfo.Address).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Phone", userInfo.Phone).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Mobile", userInfo.Mobile).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Country", userInfo.Country).SqlDbType = SqlDbType.Char;
                    command.Parameters.AddWithValue("@Zip", userInfo.Zip).SqlDbType = SqlDbType.NVarChar;
                    var retParam = command.Parameters.AddWithValue("@Return", SqlDbType.Int);
                    retParam.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)retParam.Value == 1;
                }
            }
        }

        public IEnumerable<UserInfoShort> EnumerateClients(string fio, string login, int pageNumber, int countPerPage)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_EnumerateClients";
                    command.Parameters.AddWithValue("@FIO", fio ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Login", 
                        login != null ? login.ToLower() : (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@PageNumber", pageNumber).SqlDbType = SqlDbType.Int;
                    command.Parameters.AddWithValue("@CountPerPage", countPerPage).SqlDbType = SqlDbType.Int;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        var lst = new List<UserInfoShort>();
                        while (reader.Read())
                        {
                            lst.Add(new UserInfoShort
                                {
                                    UserId = (Guid)reader["UserId"],
                                    CounOfCardAccounts = (int)reader["CounOfCardAccounts"],
                                    FIO = (string)reader["FIO"],
                                    Login = (string)reader["Login"],
                                    RegistrationDate = (DateTime)reader["RegistrationDate"]
                                });
                        }

                        return lst;
                    }
                }
            }
        }

        public UserInfo GetUser(Guid userId)
        {
            return _repository.GetUser(userId);
        }

        public bool UpdateUserInfo(UserInfo userInfo)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_UpdateUserInfo";
                    command.Parameters.AddWithValue("@UserId", userInfo.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    command.Parameters.AddWithValue("@Login", userInfo.Login).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Email", userInfo.Email).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Fio", userInfo.Fio).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Phone", userInfo.Phone).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Address", userInfo.Address).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Mobile", userInfo.Mobile).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Country", userInfo.Country).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@Zip", userInfo.Zip).SqlDbType = SqlDbType.NVarChar;
                    var returnValue = command.Parameters.Add("@Return", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)returnValue.Value == 1;
                }
            }
        }

        #endregion
    }
}