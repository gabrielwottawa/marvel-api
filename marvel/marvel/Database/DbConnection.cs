using Microsoft.Data.SqlClient;

namespace marvel.Database
{
    public class DbConnection
    {
        private string connectionString;

        public DbConnection()
        {
            this.connectionString = "Data Source=localhost\\SQLEXPRESS;Persist Security Info = True;User ID = sa;Password=sa123456;Initial Catalog = Marvel;Encrypt=True; TrustServerCertificate = True;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
