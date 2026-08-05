using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.Requests
{
    public class CreateCommentRequest
    {
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int TaskId { get; set; }

        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
    }
}
