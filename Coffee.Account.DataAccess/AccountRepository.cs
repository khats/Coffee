namespace Coffee.Account.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Security.Cryptography;
    using System.Text;

    using Coffee.Account.Domain;
    using Coffee.Administer.Domain;
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

        public UserInfo GetUser(Guid userId)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT c.UserId,c.FIO,c.Address,c.Phone,c.Mobile,c.Country,c.Zip," + 
                        "u.LoweredUserName AS Login, m.LoweredEmail AS Email FROM ClientInfo AS c " + 
                        "JOIN aspnet_Users AS u ON c.UserId = u.UserId " + 
                        "JOIN aspnet_Membership AS m ON m.UserId = c.UserId WHERE c.UserId = @userId"; 
                    command.Parameters.AddWithValue("@userId", userId).SqlDbType = SqlDbType.UniqueIdentifier;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new Exception(string.Format("Пользователь не найден|{0}", userId));
                        }

                        return new UserInfo
                            {
                                Address = (string)reader["Address"],
                                UserId = (Guid)reader["UserId"],
                                Country = (string)reader["Country"],
                                Email = (string)reader["Email"],
                                Fio = (string)reader["FIO"],
                                Login = (string)reader["Login"],
                                Mobile = (string)reader["Mobile"],
                                Phone = (string)reader["Phone"],
                                Zip = (string)reader["Zip"]
                            };
                    }
                }
            }
        }

        public bool UpdateUserPassword(Guid userId, string newPassword, string oldPassword)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                connection.Open();
                string passhash;
                string passsalt;
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Password, PasswordSalt FROM aspnet_Membership WHERE UserId=@UserId";
                    command.Parameters.AddWithValue("@UserId", userId).SqlDbType = SqlDbType.UniqueIdentifier;
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new Exception("Пользователь не найден");
                        }

                        passhash = (string)reader["Password"];
                        passsalt = (string)reader["PasswordSalt"];
                    }
                }

                var currentHash = _helper.EncodePassword(oldPassword, passsalt);
                if (currentHash != passhash)
                {
                    throw new Exception("Неверный пароль.");
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE aspnet_Membership SET Password = @pass WHERE UserId = @userId";
                    command.Parameters.AddWithValue("@pass", currentHash).SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.AddWithValue("@userId", userId).SqlDbType = SqlDbType.UniqueIdentifier;
                    var p = command.ExecuteNonQuery();
                    return p == 1;
                }
            }
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
            var saltBytes = Convert.FromBase64String(salt);
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