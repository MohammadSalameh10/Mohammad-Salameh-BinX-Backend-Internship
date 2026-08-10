using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.Requests
{
    public class CreateTaskRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
