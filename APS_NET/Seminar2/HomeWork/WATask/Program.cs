
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WATask.IAbstract;
using WATask.Models.Context;
using WATask.Repository;
using WATask.Service;

namespace WATask
{
    // Домашняя задача:
    // №1. Доработайте контроллер, реализовав в нем метод возврата CSV-файла с товарами;
    // №2. Доработайте контроллер реализовав статичный файл со статистикой работы кэш;
    // №3. Сделайте файл доступным по ссылке;
    // №4. Перенесите строку подключения для работы с базой данных в конфигурационный файл приложения. Сделано.Проверено.Работает.
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));   
            // Подключаем автомапер.
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // Подключаем автофак.
            
            var config = new ConfigurationBuilder();                                     // Подключение файла конфигурации.
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>                      // Регистрация строки подклюбчения из файла конфигурации, для файла с контекстом(работы с бд.
            {
                cb.Register(r => new ProductContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });


            builder.Host.ConfigureContainer<ContainerBuilder>(x =>                       // Через него регистрируем и связываем интрефейсы и сервисы.
            {
                x.RegisterType<ServiceProduct>().As<IServiceProduct>();
                x.RegisterType<ServiceCategory>().As<IServiceCategory>();
                x.RegisterType<ServiceFiles>().As<IServiceFiles>();
            });
            //builder.Services.AddSingleton<IServiceRepository, ServiceProduct>(); // Альтернатива автофагу - подключение и связывание интрефейса с сервесом.

            builder.Services.AddMemoryCache(x => x.TrackStatistics = true);               // Добавление кэша.

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var staticFilePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");      // Создаём строку-путь к директории, где будет храниться наш файл со статикой.
            Directory.CreateDirectory(staticFilePath);                                              // Если деиректории нет, мы её создаём.

            app.UseStaticFiles(new StaticFileOptions                                                // Сборка компонентов лоя работы с файлами в указанной директории.
            {
                FileProvider = new PhysicalFileProvider(staticFilePath), RequestPath = "/static"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
