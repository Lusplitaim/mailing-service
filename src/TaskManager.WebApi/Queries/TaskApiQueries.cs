namespace TaskManager.WebApi.Queries
{
    public static class TaskApiQueries
    {
        public static string GetAllApis => @"
            select *
            from Api";
    }
}
