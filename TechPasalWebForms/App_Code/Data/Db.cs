using System.Configuration;
using System.Data.SqlClient;

namespace TechPasalWebForms.Data
{
    public static class Db
    {
        public static SqlConnection GetConnection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["TechPasalDb"].ConnectionString;
            return new SqlConnection(connStr);
        }
    }
}
