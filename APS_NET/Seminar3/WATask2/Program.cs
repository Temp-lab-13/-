
using Autofac;
using Microsoft.EntityFrameworkCore;
using WATask2.Interface;
using WATask2.Mapper;
using WATask2.Models;
using WATask2.Query;
using WATask2.Services;

namespace WATask2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            //builder.Configuration.GetConnectionString("db"); // строка подключения.

            builder.Services.AddGraphQLServer().AddQueryType<MySimpleQuery>();
            //builder.Services.AddDbContext<ProductContext>(conf => conf.UseNpgsql(builder.Configuration.GetConnectionString("db"))); // строка подключения.
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IStoreService, StoreService>();
            builder.Services.AddTransient<ICatalogService, CatalogService>();
            //builder.Services.AddPooledDbContextFactory<ProductContext>(conf => conf.UseNpgsql(builder.Configuration.GetConnectionString("db"))); // строка подключения.2
            builder.Host.ConfigureContainer<ContainerBuilder>(cd =>
            {
                cd.Register(c => new ProductContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency(); // строка подключения.3 вроде как рабочая
            });

            var app = builder.Build();



            app.MapGraphQL();
            app.Run();
        }
    }
}
