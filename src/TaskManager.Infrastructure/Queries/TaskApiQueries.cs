namespace TaskManager.Infrastructure.Queries
{
    public static class TaskApiQueries
    {
        public static string GetAllApis => @"
            select *
            from Api";
    }
}
