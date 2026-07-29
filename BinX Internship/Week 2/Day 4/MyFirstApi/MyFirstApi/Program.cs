using MyFirstApi.Models;
namespace MyFirstApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            List<Product> minimalProducts = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 1200
                },
                new Product
                {
                    Id = 2,
                    Name = "Keyboard",
                    Price = 70
                },
                new Product
                {
                    Id = 3,
                    Name = "Mouse",
                    Price = 25
                }
            };

            // Add services to the container.

            builder.Services.AddControllers();
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

            app.UseAuthorization();


            app.MapControllers();

            app.MapGet("/minimal/products", () =>
            {
                return Results.Ok(minimalProducts);
            });

            app.MapGet("/minimal/products/{id}", (int id) =>
            {
                Product? product = minimalProducts.FirstOrDefault(product => product.Id == id);

                if (product == null)
                {
                    return Results.NotFound($"Product with ID {id} was not found.");
                }

                return Results.Ok(product);
            });

            app.Run();
        }
    }
}
