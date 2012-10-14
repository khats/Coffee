namespace Coffee.Account.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Security.Cryptography;
    using System.Text;

    using Coffee.Account.Domain;
    using Coffee.Shared.Configuration;
    using Coffee.Shared.Helper;

    public class AccountRepository : IAccountRepository
    {
        private readonly IConfigurationService _configurationService;

        private readonly ISharedHelper _helper;

        public AccountRepository(IConfigurationService configurationService, ISharedHelper helper)
        {
            _configurationService = configurationService;
            _helper = helper;
        }

        #region Implementation of IAccountRepository

        public UserAccountIdentifyInfoShort CheckUserLoginAndPassword(UserAccountIdentifyInfo identifyInfo)
        {
            var userId = this.CheckUserPass(identifyInfo.Login, identifyInfo.Password);
            if (userId == Guid.Empty)
            {
                return null;
            }

            var random = new Random();
            return new UserAccountIdentifyInfoShort
                {
                    Login = identifyInfo.Login, 
                    Password = identifyInfo.Password, 
                    Key = random.Next(1, 40)
                };
        }

        public bool Authorize(UserAccountIdentifyInfoFull identifyInfoFull)
        {
            var userId = this.CheckUserPass(identifyInfoFull.Login, identifyInfoFull.Password);
            if (userId == Guid.Empty)
            {
                return false;
            }

            string keyHash;
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SessionKeys ";
                    command.Parameters.AddWithValue("@UserId", userId).SqlDbType = SqlDbType.UniqueIdentifier;
                    connection.Open();
                    keyHash = (string)command.ExecuteScalar();
                }
            }

            return _helper.CompareStringWithHash(identifyInfoFull.Value, keyHash);
        }

        private Guid CheckUserPass(string userName, string password)
        {
            var login = userName.ToLower();
            string pass;
            string salt;
            Guid userId;
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT t1.Password, t1.PasswordSalt, t1.UserId FROM aspnet_Users as t "
                        + "INNER JOIN aspnet_Membership as t1 ON t.UserId = t1.UserId WHERE t.LoweredUserName = @Login";
                    command.Parameters.AddWithValue("@Login", login).SqlDbType = SqlDbType.NVarChar;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return Guid.Empty;
                        }

                        pass = (string)reader["Password"];
                        salt = (string)reader["PasswordSalt"];
                        userId = (Guid)reader["UserId"];
                    }
                }
            }

            var passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            var buffer = new byte[saltBytes.Length + passwordBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, buffer, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, buffer, saltBytes.Length, passwordBytes.Length);
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                var hash = Convert.ToBase64String(sha1.ComputeHash(buffer));
                return hash != pass ? Guid.Empty : userId;
            }
        }

        #endregion
    }
}