
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WATask1.DadaBase;
using WATask1.DTO;
using WATask1.Repositori;

namespace WATask1
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
            builder.Services.AddAutoMapper(typeof(MappingProfile)); // Пожключаем маппер

            var config = new ConfigurationBuilder();// Важно. Использовать тоько для подключения стороннего файла конфигурации.
            config.AddJsonFile("appsettings.json"); // подключаем файл конфигурации. 
            var cfg = config.Build();               // В частности, что бы потянуть конект-строку.

            // builder.Configuration.GetConnectionString("DataBase"); // Для поклучения инфы из родного файла конфигурации appsettings.json, можно использовать просто это

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.RegisterType<UserRepo>().As<IUserRepo>(); // Связываем интерфейсы, закидывая в контейнер
                cb.Register(c => new AppDbContext(cfg.GetConnectionString("DataBase"))).InstancePerDependency(); // Наконец-то подтягиваем конект-строку из appsettings.json
            });

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

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // Надеюсь правильно написал. Лекция 38:58

            app.Run();
        }
    }
}
