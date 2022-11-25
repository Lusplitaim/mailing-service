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

        public async Task<IEnumerable<CronTask>> GetTasksByUsername(string username)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = CronTaskQueries.GetTasksByUsername;

            IEnumerable<CronTask> tasks = await connection
                .QueryAsync<CronTask>(sql, new { Username = username });

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

        public async Task<IEnumerable<CronTask>> GetFullTasks()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = CronTaskQueries.GetFullTasks;

            IEnumerable<CronTask> tasks = await connection.QueryAsync<CronTask, AppUser, TaskApi, CronTask>(sql,
                (task, user, api) =>
                {
                    task.User = user;
                    task.Api = api;
                    return task;
                });

            return tasks;
        }

        public async Task<bool> DeleteTask(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = CronTaskQueries.DeleteTask;

            int rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

            return rowsAffected == 1 ? true : false;
        }

        public async Task<bool> UpdateExecutionDateAndCount(CronTask task, DateTime datetime)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = CronTaskQueries.UpdateExecutionDateAndCount;

            int rowsAffected = await connection.ExecuteAsync(sql, new { Id = task.Id, LastExecuted = datetime });

            return rowsAffected == 1 ? true : false;
        }

        public async Task<IEnumerable<CronTask>> GetTasksByUserId(int userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = CronTaskQueries.GetTasksByUserId;

            IEnumerable<CronTask> tasks = await connection
                .QueryAsync<CronTask>(sql, new { UserId = userId });

            return tasks;
        }
    }
}
