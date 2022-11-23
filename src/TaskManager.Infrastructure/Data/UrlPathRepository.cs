using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Queries;

namespace TaskManager.Infrastructure.Data
{
    public class UrlPathRepository
    {
        private string _connectionString;

        public UrlPathRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<UrlPath>> GetPathsByApi(int apiId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = UrlPathQueries.GetPathsByApiId;

            var paths = await connection.QueryAsync<UrlPath>(sql, new { ApiId = apiId });

            return paths;
        }
    }
}
