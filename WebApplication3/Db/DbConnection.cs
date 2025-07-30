using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;


namespace WebApplication3.Db
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection(IOptions<DbSettings> dbSettings)
        {
            _connectionString = dbSettings.Value.ConnectionString;
        }

        public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
