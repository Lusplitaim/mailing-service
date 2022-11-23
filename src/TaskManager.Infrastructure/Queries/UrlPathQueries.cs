using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Queries
{
    public static class UrlPathQueries
    {
        public static string GetPathsByApiId => @"
            select * from UrlPaths
            where apiId = @ApiId;";
    }
}
