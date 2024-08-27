
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
                .AddQueryType<MySimpleQuery>()                                      // ���������� ����QL, � ��������� ��� ��� ������ ���  Query-����, ��� ������������ ������� �� ��������� ������,
                .AddMutationType<MiSimpleMutation>();                               // � mutation-����, ��� ������������� ������� �� ���������� ��� ��������� ������

            builder.Services.AddMemoryCache();                                      // ���������� ���.
            builder.Services.AddAutoMapper(typeof(MappingProfile));                 // ���������� ���������� � ��������� �� ��� ������-����.
            builder.Services.AddTransient<IProductService, ProductService>();       // ���������� �������.
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
