namespace TaskManager.Core.Models
{
    public class TaskApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<UrlPath> UrlPaths { get; set; }
    }
}
