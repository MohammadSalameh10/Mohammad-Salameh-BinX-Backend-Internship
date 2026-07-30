using MiddlewareDependencyInjectionDay5.Models;
using MiddlewareDependencyInjectionDay5.Middleware;
using MiddlewareDependencyInjectionDay5.Services;

namespace MiddlewareDependencyInjectionDay5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IProductService, ProductService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
