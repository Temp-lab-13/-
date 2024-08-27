
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WATask2.Interface;
using WATask2.Mapper;
using WATask2.Models;
using WATask2.Mutatin;
using WATask2.Query;
using WATask2.Services;

namespace WATask2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGraphQLServer()
                .AddQueryType<MySimpleQuery>()                                      // Подключаем ГрафQL, и указываем ему для работы наш  Query-файл, где поизводяться запросы на получение данных,
                .AddMutationType<MiSimpleMutation>();                               // и mutation-файл, где производяться запросы на добавление или изменение данных

            builder.Services.AddMemoryCache();                                      // Подключаем кэш.
            builder.Services.AddAutoMapper(typeof(MappingProfile));                 // Подключаем автомаппер и ссылаемся на наш мапинг-файл.
            builder.Services.AddTransient<IProductService, ProductService>();       // Подключаем сервисы.
            //builder.Services.AddTransient<IStoreService, StoreService>();            
            builder.Services.AddTransient<ICatalogService, CatalogService>();
            builder.Services.AddEndpointsApiExplorer(); // ?
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cd =>
            {
                cd.Register(c => new ProductContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });

            var app = builder.Build();

            app.MapGraphQL();
            app.Run();
        }
    }
}
