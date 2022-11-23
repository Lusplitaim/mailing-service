namespace TaskManager.Infrastructure.Queries
{
    public static class TaskApiQueries
    {
        public static string GetApis => @"
            select *
            from Api";
    }
}
