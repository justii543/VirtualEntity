using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.VirtualEntity
{
    public class PolicyAzureDB
    {
        public static SqlConnection PolicyAzureDb()
        {
            SqlConnectionStringBuilder connstring = new SqlConnectionStringBuilder();
            connstring.DataSource = "newindiasqldatabase.database.windows.net";
            connstring.UserID = "CloudSA740fed1f";
            connstring.Password = "hitachi007#";
            connstring.InitialCatalog = "AzureSqlDatabse";

            SqlConnection connection = new SqlConnection(connstring.ConnectionString);
            return connection;
        }
    }
}
