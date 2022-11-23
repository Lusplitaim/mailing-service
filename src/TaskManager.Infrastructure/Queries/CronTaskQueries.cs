namespace TaskManager.Infrastructure.Queries
{
    public static class CronTaskQueries
    {
        public static string CreateTask => @"
            insert into Tasks (name, description, minutes, hours, days, months, weekdays, userId, apiId)
            values (@Name, @Description, @Minutes, @Hours, @Days, @Months, @Weekdays, @UserId, @ApiId)";

        public static string GetTasks => @"
            select * from Tasks;";

        public static string GetTasksByUsername => @"
            select * from Tasks t
            join Users u on t.userId = u.id
            where u.username = @Username;";

        public static string GetFullTasks => @"
            select * from Tasks t
            join Users u on u.id = t.userId
            join Api a on a.id = t.apiId;";
    }
}
