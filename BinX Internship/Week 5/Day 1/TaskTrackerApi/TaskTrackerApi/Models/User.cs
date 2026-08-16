namespace TaskTrackerApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
