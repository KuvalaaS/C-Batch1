using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickectBookingSystem_Project.Util
{
    internal class DBUtil
    {
        private static string ConnectionString = "Server=KUVALA\\SQLEXPRESS;Database=TicketBookingSystemProject;Integrated Security=True;";

        public static SqlConnection GetDBConn()
        {
            return new SqlConnection(ConnectionString);

        }
    }
}
