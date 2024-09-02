
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WATask.IAbstract;
using WATask.Models.Context;
using WATask.Mutation;
using WATask.Querty;
using WATask.Repository;
using WATask.Service;
//https://localhost:7164/swagger/index.html
namespace WATask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGraphQLServer().AddQueryType<MySimpleQuery>().AddMutationType<MiSimpleMutation>(); //   Загружаем схемы querty and Mutation, для работе с Графом.


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));                      // Подключаем автомапер.
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // Подключаем автофак.
            
            var config = new ConfigurationBuilder();                                     // Подключение файла конфигурации.
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>                      // Регистрация строки подклюбчения из файла конфигурации, для файла с контекстом(работы с бд.
            {
                cb.Register(r => new ProductContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Services.AddTransient<IServiceProduct, ServiceProduct>();
            builder.Services.AddTransient<IServiceCategory, ServiceCategory>();
            builder.Services.AddTransient<IServiceFiles, ServiceFiles>();
            /*builder.Host.ConfigureContainer<ContainerBuilder>(x =>                       // Через него регистрируем и связываем интрефейсы и сервисы.
            {
                x.RegisterType<ServiceProduct>().As<IServiceProduct>();
                x.RegisterType<ServiceCategory>().As<IServiceCategory>();
                x.RegisterType<ServiceFiles>().As<IServiceFiles>();
            });*/
 

            builder.Services.AddMemoryCache(x => x.TrackStatistics = true);               // Добавление кэша.

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var staticFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");      // Создаём строку-путь к директории, где будет храниться наш файл со статикой.
            Directory.CreateDirectory(staticFilePath);                                              // Если деиректории нет, мы её создаём.

            app.UseStaticFiles(new StaticFileOptions                                                // Сборка компонентов лоя работы с файлами в указанной директории.
            {
                FileProvider = new PhysicalFileProvider(staticFilePath), RequestPath = "/static"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.MapGraphQL();
            app.Run();
        }
    }
}
