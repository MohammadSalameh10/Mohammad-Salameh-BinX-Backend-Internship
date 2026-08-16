using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.Models;
using TaskTrackerApi.Requests;
using TaskTrackerApi.Services.Interfaces;

namespace TaskTrackerApi.Services.Classes
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(task => task.Id == id);
        }

        public async Task<TaskItem> CreateAsync(
            CreateTaskRequest request)
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                DueDate = request.DueDate
            };

            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateTaskRequest request)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(task => task.Id == id);

            if (task is null)
            {
                return false;
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.UserId = request.UserId;
            task.DueDate = request.DueDate;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(task => task.Id == id);

            if (task is null)
            {
                return false;
            }

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
