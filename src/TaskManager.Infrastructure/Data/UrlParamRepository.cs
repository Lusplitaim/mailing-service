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
    public class UrlParamRepository
    {
        private string _connectionString;

        public UrlParamRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<UrlParam>> GetParamsByPath(int pathId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = UrlParamQueries.GetParamsByPathId;

            var urlParams = await connection.QueryAsync<UrlParam>(sql, new { PathId = pathId });

            return urlParams;
        }
    }
}
