namespace TaskManager.Infrastructure.Queries
{
    public static class CronTaskQueries
    {
        public static string CreateTask => @"
            insert into Tasks (name, description, minutes, hours, days, months, weekdays, urlParamsString, userId, apiId)
            values (@Name, @Description, @Minutes, @Hours, @Days, @Months, @Weekdays, @UrlParamsString, @UserId, @ApiId)";

        public static string GetTasks => @"
            select * from Tasks;";

        public static string GetTasksByUsername => @"
            select t.id, t.name, t.description, t.minutes, t.hours, t.days, t.months, t.weekdays, t.userId, t.apiId
            from Tasks t
            join Users u on u.id = t.userId
            where @Username = u.username;";

        public static string GetFullTasks => @"
            select * from Tasks t
            join Users u on u.id = t.userId
            join Api a on a.id = t.apiId;";

        public static string DeleteTask => @"
            delete from Tasks
            where id = @Id";

        public static string UpdateExecutionDate => @"
            update Tasks
            set lastExecuted = @LastExecuted
            where id = @Id;";
    }
}
