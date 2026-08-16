using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.Models;
using TaskTrackerApi.Requests;
using TaskTrackerApi.Services.Interfaces;

namespace TaskTrackerApi.Services.Classes
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(comment => comment.Id == id);
        }

        public async Task<Comment> CreateAsync(
            CreateCommentRequest request)
        {
            var comment = new Comment
            {
                Content = request.Content,
                TaskId = request.TaskId,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);

            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateCommentRequest request)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(comment => comment.Id == id);

            if (comment is null)
            {
                return false;
            }

            comment.Content = request.Content;
            comment.TaskId = request.TaskId;
            comment.UserId = request.UserId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(comment => comment.Id == id);

            if (comment is null)
            {
                return false;
            }

            _context.Comments.Remove(comment);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
