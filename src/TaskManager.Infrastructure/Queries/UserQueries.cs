namespace TaskManager.Infrastructure.Queries
{
    public static class UserQueries
    {
        public static string AllUsers => @"
            select *
            from Users u
            join Roles r on r.id = u.roleId;";

        public static string GetUserByEmail => @"
            select *
            from Users u
            join Roles r on r.id = u.roleId
            where email = @email;";

        public static string GetUserByUsername => @"
            select *
            from Users u
            join Roles r on r.id = u.roleId
            where username = @username;";

        public static string CreateUser => @"
            insert into Users (username, email, passwordHash, passwordSalt)
            values (@Username, @Email, @PasswordHash, @PasswordSalt);";


    }
}
