
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WATaskStoreg.IAbstract;
using WATaskStoreg.Models.Context;
using WATaskStoreg.Mutation;
using WATaskStoreg.Querty;
using WATaskStoreg.Repository;
using WATaskStoreg.Service;
using WATaskStoreg.WebClient.Client;
using WATaskStoreg.WebClient.IAbstractClient;

namespace WATaskStoreg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGraphQLServer().AddQueryType<MySimpleQuery>().AddMutationType<MiSimpleMutation>(); // Загружаем схемы querty and Mutation, для работе с Графом.

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
                cb.Register(r => new StoregeContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Host.ConfigureContainer<ContainerBuilder>(x =>                       // Регистрируем и связываем интрефейсы и сервисы.
            {
                x.RegisterType<ServiceStorage>().As<IServiceStorage>();
                x.RegisterType<StoregClient>().As<IStoregClient>();
            });



            builder.Services.AddMemoryCache(x => x.TrackStatistics = true);

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

            app.MapGraphQL(); 
            app.Run();
        }
    }
}
