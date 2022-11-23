using Dapper;
using Microsoft.Data.Sqlite;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Queries;

namespace TaskManager.Infrastructure.Data
{
    public class ApiRepository
    {
        private string _connectionString;

        public ApiRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TaskApi>> GetApis()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = TaskApiQueries.GetApis;

            var apis = await connection.QueryAsync<TaskApi>(sql);

            return apis;
        }
    }
}