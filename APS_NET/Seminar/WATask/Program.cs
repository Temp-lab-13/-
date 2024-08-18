
using Microsoft.EntityFrameworkCore;
using WATask.Models;

namespace WATask
{
    // Домашняя задача:
    // Доработайте контроллер, дополнив его возможностью удалять группы и продукты, а также задавать цены.
    // Для каждого типа ответа создайте свою модель.
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext <ProductContext> (opt =>
             opt.UseNpgsql("Host=localhost;Username=postgres;Password=lotta;Database=AppStoreg"));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
