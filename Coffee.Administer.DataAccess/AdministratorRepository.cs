namespace Coffee.Administer.DataAccess
{
    using System.Data;
    using System.Data.SqlClient;

    using Coffee.Administer.Domain;
    using Coffee.Shared.Configuration;
    using Coffee.Shared.Helper;

    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly IConfigurationService _configurationService;

        private readonly ISharedHelper _helper;

        public AdministratorRepository(IConfigurationService configurationService, ISharedHelper helper)
        {
            _configurationService = configurationService;
            _helper = helper;
        }

        #region Implementation of IAdministratorRepository

        public bool CreateUser(UserInfoCreation userInfo)
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
                    command.Parameters.AddWithValue("@Coutnry", userInfo.Country).SqlDbType = SqlDbType.Char;
                    command.Parameters.AddWithValue("@Zip", userInfo.Zip).SqlDbType = SqlDbType.NVarChar;
                    var retParam = command.Parameters.AddWithValue("@Return", SqlDbType.Int);
                    retParam.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)retParam.Value == 1;
                }
            }

            // TODO INVOKE SERVICE
        }

        #endregion
    }
}