using Dapper;
using Microsoft.Data.Sqlite;
using TaskManager.WebApi.Models;
using TaskManager.WebApi.Queries;

namespace TaskManager.WebApi.Data
{
    public class CronTaskRepository
    {
        private string _connectionString;

        public CronTaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> CreateTask(CronTask task)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = CronTaskQueries.CreateTask;

            int rowsAffected = await connection.ExecuteAsync(sql, task);

            return rowsAffected == 1 ? true : false;
        }
    }
}
