using TaskTrackerApi.Models;
using TaskTrackerApi.Requests;

namespace TaskTrackerApi.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllAsync();

        Task<TaskItem?> GetByIdAsync(int id);

        Task<TaskItem> CreateAsync(CreateTaskRequest request);

        Task<bool> UpdateAsync(int id, UpdateTaskRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
