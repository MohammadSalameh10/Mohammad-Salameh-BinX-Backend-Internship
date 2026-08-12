namespace TaskTrackerApi.Requests
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Status { get; set; } = string.Empty;

        public int UserId { get; set; }

        public DateTime? DueDate { get; set; }
    }
}