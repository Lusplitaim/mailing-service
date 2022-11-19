using System.Data.Common;
using System.Data;
using Microsoft.Data.Sqlite;

namespace TaskManager.WebApi.Data
{
    public class DatabaseContext
    {
        private IConfiguration _config;
        private SqliteConnection _connection;

        public UserRepository UserRepository { get; private set; }

        public DatabaseContext(IConfiguration config)
        {
            _config = config;
            InitRepositories();
        }

        public void Connect()
        {
            if (ConnectionIsClosed())
            {
                _connection.Open();
            }
        }

        private bool ConnectionIsClosed()
        {
            return _connection.State == ConnectionState.Closed;
        }

        public void Disconnect()
        {
            _connection.Close();
        }

        private void InitRepositories()
        {
            _connection = CreateConnection();

            UserRepository = new UserRepository(_connection);
        }

        private SqliteConnection CreateConnection()
        {
            var databasePath = GetDatabasePath();
            string connectionString = string.Format("Data Source={0};", databasePath);
            return new SqliteConnection(connectionString);
        }

        private string GetDatabasePath()
        {
            return _config["DatabasePath"];
        }
    }
}
