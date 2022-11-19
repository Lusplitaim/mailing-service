using Dapper;
using Microsoft.Data.Sqlite;
using TaskManager.WebApi.Models;

namespace TaskManager.WebApi.Data
{
    public class UserRepository
    {
        private SqliteConnection _connection;

        public UserRepository(SqliteConnection connection)
        {
            _connection = connection;
        }

        public async Task<User> CreateUser(User user)
        {
            string sql = @"
                insert into Users (username, email, passwordHash, passwordSalt)
                values (@Username, @Email, @PasswordHash, @PasswordSalt);";

            int rowsAffected = await _connection.ExecuteAsync(sql, user);

            User? createdUser = await GetUserByName(user.Username);

            if (createdUser is null) throw new Exception("Could not create user");

            return createdUser;
        }

        public async Task<User?> GetUserByName(string username)
        {
            string sql = @"
                select id, username, email, passwordHash, passwordSalt
                from Users
                where username = @username;";

            var users = await _connection.QueryAsync<User>(sql, new { username });

            return users.FirstOrDefault();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            string sql = @"
                select id, username, email, passwordHash, passwordSalt
                from Users
                where email = @email;";

            var users = await _connection.QueryAsync<User>(sql, new { email });

            return users.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {            
            string sql = @"
                select id, username, email, passwordHash, passwordSalt
                from Users;";

            var users = await _connection.QueryAsync<User>(sql);

            return users;
        }
    }
}
