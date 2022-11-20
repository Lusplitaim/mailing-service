namespace TaskManager.Application.DTO
{
    public class CronTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Minutes { get; set; }
        public string Hours { get; set; }
        public string Days { get; set; }
        public string Months { get; set; }
        public string Weekdays { get; set; }
        public int UserId { get; set; }
        public int ApiId { get; set; }
    }
}
