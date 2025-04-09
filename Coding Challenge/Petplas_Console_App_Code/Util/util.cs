using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Util
{
    public class DBConnUtil
    {
        static SqlConnection con;

        public static SqlConnection GetConnection()
        {
            // Initializes connection to the SQL Server database
            con = new SqlConnection("data source=KUVALA\\SQLEXPRESS;initial catalog=PetPals;integrated security=true");
            con.Open(); // Opens the connection
            return con; // Returns the connection object to the caller
        }
    }
}
