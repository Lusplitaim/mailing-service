﻿namespace TaskManager.WebApi.Queries
{
    public static class CronTaskQueries
    {
        public static string CreateTask => @"
            insert into Tasks (name, description, minutes, hours, days, months, weekdays, userId, apiId)
            values (@Name, @Description, @Minutes, @Hours, @Days, @Months, @Weekdays, @UserId, @ApiId)";
    }
}
