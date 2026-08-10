using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();

        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(user => user.Id);

                entity.Property(user => user.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(user => user.Email)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.HasIndex(user => user.Email)
                    .IsUnique();
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasKey(task => task.Id);

                entity.Property(task => task.Title)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(task => task.Description)
                    .HasMaxLength(1000);

                entity.Property(task => task.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(task => task.CreatedAt)
                    .IsRequired();

                entity.HasOne(task => task.User)
                    .WithMany(user => user.Tasks)
                    .HasForeignKey(task => task.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");

                entity.HasKey(comment => comment.Id);

                entity.Property(comment => comment.Content)
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.Property(comment => comment.CreatedAt)
                    .IsRequired();

                entity.HasOne(comment => comment.Task)
                    .WithMany(task => task.Comments)
                    .HasForeignKey(comment => comment.TaskId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(comment => comment.User)
                    .WithMany(user => user.Comments)
                    .HasForeignKey(comment => comment.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

    }
}
