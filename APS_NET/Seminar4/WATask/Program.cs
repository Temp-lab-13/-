
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WATask.Models.Abstract;
using WATask.Repo;

namespace WATask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile)); // ���������� ������ � ����� ������ ���������
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // ��������� �������
            builder.Host.ConfigureContainer<ContainerBuilder>(b =>
            {
                b.RegisterType<ProductRepo>().As<IProductRepo>();
            });
            builder.Services.AddMemoryCache(m => m.TrackStatistics = true);
            // builder.Services.AddSingleton<IProductRepo, ProductRepo>(); ������� ������������ ����������� ������. ����� ������������ ���.
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
