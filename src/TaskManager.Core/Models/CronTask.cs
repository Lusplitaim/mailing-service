using System.Runtime.CompilerServices;

namespace TaskManager.Core.Models
{
    public class CronTask
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
        public AppUser? User { get; set; }


        public static implicit operator CronDate(CronTask task)
        {
            return new CronDate()
            {
                Minutes = task.Minutes,
                Hours = task.Hours,
                Days = task.Days,
                Months = task.Months,
                Weekdays = task.Weekdays
            };
        }
    }
}
