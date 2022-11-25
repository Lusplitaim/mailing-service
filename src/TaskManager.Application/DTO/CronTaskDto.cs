using TaskManager.Core.Models;

namespace TaskManager.Application.DTO
{
    public class CronTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Minutes { get; set; }
        public string Hours { get; set; }
        public string Days { get; set; }
        public string Months { get; set; }
        public string Weekdays { get; set; }
        public string? UrlParamsString { get; set; }
        public DateTime? LastExecuted { get; set; }
        public int ExecutionCount { get; set; }
        public int UserId { get; set; }
        public int ApiId { get; set; }
        public TaskApi? Api { get; set; }
        public AppUserDto? User { get; set; }
    }
}
