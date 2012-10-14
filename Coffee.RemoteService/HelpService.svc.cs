namespace Coffee.RemoteService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Coffee.Shared.Helper;

    public class HelpService : IHelpService
    {
        private readonly ISharedHelper _helper;

        public HelpService(ISharedHelper helper)
        {
            _helper = helper;
        }

        #region Implementation of IHelpService

        public void CreateAssociation(Guid userId)
        {
            var dataTable = new DataTable("Codes");
            dataTable.Columns.Add("CodeNumber", typeof(byte));
            dataTable.Columns.Add("CodeValue", typeof(string));
            var pins = new Dictionary<byte, int>();
            var random = new Random();
            for (byte i = 0; i < 40; i++)
            {
                var pin = random.Next(1000, 9999);
                pins.Add(i, pin);
                dataTable.Rows.Add(i, this._helper.ComputeHashFromString(pin.ToString(CultureInfo.InvariantCulture)));
            }

            int r;
            using (var connection = new SqlConnection())
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

            if (r == 1)
            {
                var filename = Path.Combine("c:\\", userId.ToString("N"), ".txt");
                File.WriteAllLines(filename, pins.Select(x => string.Concat(x.Key, " - ", x.Value)));
            }
            else
            {
                // TODO log error
            }
        }

        #endregion
    }
}
