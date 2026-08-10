using TaskTrackerApi.Models;
using TaskTrackerApi.Requests;

namespace TaskTrackerApi.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(int id);

        Task<Comment> CreateAsync(CreateCommentRequest request);

        Task<bool> UpdateAsync(
            int id,
            UpdateCommentRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
