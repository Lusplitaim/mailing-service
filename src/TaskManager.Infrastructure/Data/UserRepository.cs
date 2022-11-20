using Dapper;
using Microsoft.Data.Sqlite;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Queries;

namespace TaskManager.Infrastructure.Data
{
    public class UserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> CreateUser(User user)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = UserQueries.CreateUser;

            int rowsAffected = await connection.ExecuteAsync(sql, user);

            User? createdUser = await GetUserByName(user.Username);

            if (createdUser is null) throw new Exception("Could not create user");

            return createdUser;
        }

        public async Task<User?> GetUserByName(string username)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = UserQueries.GetUserByUsername;

            var users = await connection.QueryAsync<User>(sql, new { username });

            return users.FirstOrDefault();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = UserQueries.GetUserByEmail;

            var users = await connection.QueryAsync<User>(sql, new { email });

            return users.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = UserQueries.AllUsers;

            var users = await connection.QueryAsync<User>(sql);

            return users;
        }
    }
}
