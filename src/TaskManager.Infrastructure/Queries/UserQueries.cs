namespace TaskManager.Infrastructure.Queries
{
    public static class UserQueries
    {
        public static string AllUsers => @"
            select *
            from Users;";

        public static string GetUserByEmail => @"
            select *
            from Users
            where email = @email;";

        public static string GetUserByUsername => @"
            select *
            from Users
            where username = @username;";

        public static string CreateUser => @"
            insert into Users (username, email, passwordHash, passwordSalt)
            values (@Username, @Email, @PasswordHash, @PasswordSalt);";


    }
}
