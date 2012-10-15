namespace Coffee.RemoteService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    using Coffee.Shared.Helper;

    public class HelpService : IHelpService
    {
        private readonly string _connectionString;

        private readonly ISharedHelper _helper;

        public HelpService(ISharedHelper helper)
        {
            _helper = helper;
            _connectionString = "Data Source=195.50.21.74;Initial Catalog=Coffee;Persist Security Info=True;"
                                + "User ID=database;Password=5E43zdwNKV";
        }

        #region Implementation of IHelpService

        public void CreateAssociation(Guid userId)
        {
            var dataTable = new DataTable("Codes");
            dataTable.Columns.Add("CodeNumber", typeof(byte));
            dataTable.Columns.Add("CodeValue", typeof(string));
            var pins = new Dictionary<byte, string>();
            var random = new Random();
            for (byte i = 1; i < 41; i++)
            {
                var pin = random.Next(1000, 9999);
                var stringPin = pin.ToString(CultureInfo.InvariantCulture);
                pins.Add(i, stringPin);
                dataTable.Rows.Add(i, this._helper.ComputeHashFromString(stringPin));
            }

            int r;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_AssociateUserWithCodeCard";
                    command.Parameters.AddWithValue("@UserId", userId).SqlDbType = SqlDbType.UniqueIdentifier;
                    command.Parameters.AddWithValue("@Codes", dataTable).SqlDbType = SqlDbType.Structured;
                    var retParam = command.Parameters.Add("@Return", SqlDbType.Int);
                    retParam.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    r = (int)retParam.Value;
                }
            }

            if (r != 1)
            {
                throw new Exception();
            }

            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM OpenCodes WHERE UserId = @userId";
                    command.Parameters.AddWithValue("@userId", userId).SqlDbType = SqlDbType.UniqueIdentifier;
                    command.ExecuteNonQuery();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "INSERT INTO OpenCodes(UserId, CodeId, Code) VALUES(@userId, @codeId, @codeVal)";
                    command.Parameters.AddWithValue("@userId", userId).SqlDbType = SqlDbType.UniqueIdentifier;
                    var codeParam = command.Parameters.Add("@codeId", SqlDbType.TinyInt);
                    var valParam = command.Parameters.Add("@codeVal", SqlDbType.NChar, 4);
                    foreach (var pin in pins)
                    {
                        codeParam.Value = pin.Key;
                        valParam.Value = pin.Value;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        #endregion
    }
}
