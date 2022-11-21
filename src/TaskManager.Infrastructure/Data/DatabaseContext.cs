namespace TaskManager.Infrastructure.Data
{
    public class DatabaseContext
    {
        private string _connectionString;

        public UserRepository UserRepository { get; private set; }
        public ApiRepository ApiRepository { get; private set; }
        public CronTaskRepository CronTaskRepository { get; private set; }

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
            InitRepositories();
        }

        private void InitRepositories()
        {
            UserRepository = new UserRepository(_connectionString);
            ApiRepository = new ApiRepository(_connectionString);
            CronTaskRepository = new CronTaskRepository(_connectionString);
        }
    }
}
