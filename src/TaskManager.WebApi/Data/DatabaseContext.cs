using System.Data.Common;
using System.Data;
using Microsoft.Data.Sqlite;

namespace TaskManager.WebApi.Data
{
    public class DatabaseContext
    {
        private IConfiguration _config;
        private string _connectionString;

        public UserRepository UserRepository { get; private set; }
        public ApiRepository ApiRepository { get; private set; }

        public DatabaseContext(IConfiguration config)
        {
            _config = config;
            InitRepositories();
        }

        private void InitRepositories()
        {
            _connectionString = CreateConnectionString();

            UserRepository = new UserRepository(_connectionString);
            ApiRepository = new ApiRepository(_connectionString);
        }

        private string CreateConnectionString()
        {
            var databasePath = GetDatabasePath();
            return string.Format("Data Source={0};", databasePath);
        }

        private string GetDatabasePath()
        {
            return _config["DatabasePath"];
        }
    }
}
