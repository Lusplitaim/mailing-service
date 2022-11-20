using Dapper;
using Microsoft.Data.Sqlite;
using TaskManager.WebApi.Models;
using TaskManager.WebApi.Queries;

namespace TaskManager.WebApi.Data
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

            string sql = TaskApiQueries.GetAllApis;

            var apis = await connection.QueryAsync<TaskApi>(sql);

            return apis;
        }
    }
}