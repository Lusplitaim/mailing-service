using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Queries
{
    public static class UrlParamQueries
    {
        public static string GetParamsByPathId => @"
            select * from UrlParams
            where pathId = @PathId;";
    }
}
