
using Autofac.Extensions.DependencyInjection;
using Autofac;
using WATask2.DTO;
using WATask2.Repositiri;
using WATask2.Db;

namespace WATask2
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

            //----------------------------------------------------------------------
            builder.Services.AddAutoMapper(typeof(MappingProfile)); // Пожключаем маппер
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.RegisterType<LiberyRepo>().As<ILiberyRepo>(); // Связываем интерфейсы, закидывая в контейнер
                cb.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("DataBase"))).InstancePerDependency(); // подтягиваем конект-строку из appsettings.json, упрощённый способ, для родного файла конфига
            });
            //----------------------------------------------------------------------

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // dotnet ef migrations add InitialCreate --context AppDbContext
            // dotnet ef database update --connection "строка подключения"
            app.MapControllers();

            app.Run();
        }
    }
}
