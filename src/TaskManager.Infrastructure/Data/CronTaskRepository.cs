using Dapper;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Queries;

namespace TaskManager.Infrastructure.Data
{
    public class CronTaskRepository
    {
        private string _connectionString;

        public CronTaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CronTask>> GetTasks()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = CronTaskQueries.GetTasks;

            IEnumerable<CronTask> tasks = await connection.QueryAsync<CronTask>(sql);

            return tasks;
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
